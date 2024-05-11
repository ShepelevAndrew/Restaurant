using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Orders.Entities;
using Restaurant.Domain.Orders.Repositories;
using Serilog;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly RestaurantDbContext _dbContext;
    private readonly ILogger _logger;

    public OrderDetailRepository(RestaurantDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> Create(OrderDetail orderDetail)
    {
        FormattableString command =
            $"INSERT INTO public.\"OrderDetails\" (\"OrderDetailId\", \"OrderId\", \"ProductId\", \"ProductName\", \"Quantity\", \"Price\") VALUES ({orderDetail.OrderDetailId}, {orderDetail.OrderId}, {orderDetail.ProductId}, {orderDetail.ProductName}, {orderDetail.Quantity}, {orderDetail.Price})";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in OrderDetailRepository.");
            return false;
        }
    }

    public async Task<bool> Create(IEnumerable<OrderDetail> orderDetails)
    {
        var isSuccess = true;

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        foreach (var orderDetail in orderDetails)
        {
            isSuccess = await Create(orderDetail);

            if(!isSuccess)
                break;
        }

        if (isSuccess)
            await transaction.CommitAsync();
        else
            await transaction.RollbackAsync();

        return isSuccess;
    }

    public async Task<OrderDetail?> GetById(Guid orderDetailId)
    {
        FormattableString query = $"SELECT * FROM public.\"OrderDetails\" WHERE \"OrderDetailId\" = {orderDetailId}";
        try
        {
            var orderDetail = await _dbContext.OrderDetails
                .FromSql(query)
                .SingleOrDefaultAsync();

            return orderDetail;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in OrderDetailRepository.");
            throw;
        }
    }

    public async Task<bool> Delete(Guid orderDetailId)
    {
        FormattableString command = $"DELETE FROM public.\"OrderDetails\" WHERE \"OrderDetailId\" = {orderDetailId}";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in OrderDetailRepository.");
            return false;
        }
    }
}