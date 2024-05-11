using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Orders.Entities;
using Restaurant.Domain.Orders.Repositories;

namespace Restaurant.Application.Orders.GetOrderDetailsFromCart;

public class GetOrderDetailsFromCartHandler : IRequestHandler<GetOrderDetailsFromCart, ErrorOr<List<OrderDetail>>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderDetailsFromCartHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<List<OrderDetail>>> Handle(GetOrderDetailsFromCart request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderFromCart(request.UserId);
        if (order is not null)
        {
            return order.OrderDetails.ToList();
        }

        var isSuccess = await _orderRepository.Create(new(request.UserId));
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        var createdOrder = await _orderRepository.GetOrderFromCart(request.UserId);
        if (createdOrder is null)
        {
            return Errors.Order.OrderNotFound;
        }

        return createdOrder.OrderDetails.ToList();
    }
}