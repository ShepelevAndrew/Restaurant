using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Enums;
using Restaurant.Domain.Orders.Repositories;
using Serilog;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly ILogger _logger;

    public OrderRepository(RestaurantDbContext dbContext, IOrderDetailRepository orderDetailRepository, ILogger logger)
    {
        _dbContext = dbContext;
        _orderDetailRepository = orderDetailRepository;
        _logger = logger;
    }

    public async Task<bool> Create(Order order)
    {
        FormattableString command =
            $"INSERT INTO public.\"Orders\" (\"OrderId\", \"Sum\", \"BuyDate\", \"ConfirmedDate\", \"CookedDate\", \"ShippedDate\", \"PaidDate\", \"CancelledDate\", \"Status\", \"UserId\") VALUES ({order.OrderId}, {order.Sum}, {order.BuyDate}, {order.ConfirmedDate}, {order.CookedDate}, {order.ShippedDate}, {order.PaidDate}, {order.CancelledDate}, {order.Status}, {order.UserId})";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            var isSuccess = await _orderDetailRepository.Create(order.OrderDetails);
            if (!isSuccess)
                return false;

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in OrderRepository.");
            return false;
        }
    }

    public async Task<Order?> GetOrder(Guid orderId)
    {
        FormattableString query = $"SELECT * FROM public.\"Orders\" WHERE \"OrderId\" = {orderId}";
        try
        {
            var order = await _dbContext.Orders
                .FromSql(query)
                .Include(o => o.OrderDetails)
                .SingleOrDefaultAsync();

            return order;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in OrderRepository.");
            throw;
        }
    }

    public async Task<Order?> GetOrderFromCart(Guid userId)
    {
        FormattableString query = $"SELECT * FROM public.\"Orders\" WHERE \"UserId\" = {userId} AND \"Status\" = {OrderStatus.Open}";
        try
        {
            var order = await _dbContext.Orders
                .FromSql(query)
                .Include(o => o.OrderDetails)
                .SingleOrDefaultAsync();

            return order;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in OrderRepository.");
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersWithStatus(OrderStatus status)
    {
        FormattableString query = $"SELECT * FROM public.\"Orders\" WHERE \"Status\" = {status}";
        try
        {
            var orders = await _dbContext.Orders
                .FromSql(query)
                .Include(o => o.OrderDetails)
                .ToListAsync();

            return orders;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in OrderRepository.");
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserId(Guid userId)
    {
        FormattableString query = $"SELECT * FROM public.\"Orders\" WHERE \"UserId\" = {userId}";
        try
        {
            var orders = await _dbContext.Orders
                .FromSql(query)
                .Include(o => o.OrderDetails)
                .ToListAsync();

            return orders;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in OrderRepository.");
            throw;
        }
    }

    public async Task<bool> UpdateOrderStatusInOrder(Order order)
    {
        FormattableString command =
            $"UPDATE public.\"Orders\" SET \"Status\" = {order.Status} WHERE \"OrderId\" = {order.OrderId}";
        try
        {
            var rawCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawCount > 0;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql in OrderRepository.");
            return false;
        }
    }

    public async Task<bool> UpdateOrderInfoInOrder(Order order)
    {
        FormattableString command =
            $"UPDATE public.\"Orders\" SET \"Location\" = {order.Location}";
        try
        {
            var rawCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawCount > 0;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql in OrderRepository.");
            return false;
        }
    }

    public async Task<bool> UpdateOrderDetailsInOrder(Order order)
    {
        FormattableString command =
            $"UPDATE public.\"Orders\" SET \"Sum\" = {order.Sum} WHERE \"OrderId\" = {order.OrderId}";
        try
        {
            var rawCount = await _dbContext.Database.ExecuteSqlAsync(command);
            var orderDetails = order.OrderDetails;

            foreach (var orderDetail in orderDetails)
            {
                var orderDetailFromDb = await _orderDetailRepository.GetById(orderDetail.OrderDetailId);
                if (orderDetailFromDb is not null)
                {
                    continue;
                }

                var isSuccess = await _orderDetailRepository.Create(orderDetail);
                if (!isSuccess)
                {
                    return false;
                }
            }

            return rawCount > 0;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error with add order detail in order in OrderRepository.");
            return false;
        }
    }
}