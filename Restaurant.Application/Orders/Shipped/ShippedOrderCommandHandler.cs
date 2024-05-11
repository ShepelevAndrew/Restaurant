using ErrorOr;
using MediatR;
using Restaurant.Application.Common.Abstractions.NotificationSenders;
using Restaurant.Application.Orders.Verification;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Enums;
using Restaurant.Domain.Orders.Repositories;

namespace Restaurant.Application.Orders.Shipped;

public class ShippedOrderCommandHandler
    : IRequestHandler<ShippedOrderCommand, ErrorOr<Order>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly INotificationSender _notificationSender;

    public ShippedOrderCommandHandler(
        IOrderRepository orderRepository,
        INotificationSender notificationSender)
    {
        _orderRepository = orderRepository;
        _notificationSender = notificationSender;
    }

    public async Task<ErrorOr<Order>> Handle(ShippedOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(request.OrderId);
        if (order is null)
        {
            return Errors.Order.OrderNotFound;
        }

        order.ChangeOrderStatus(OrderStatus.Shipped);

        var isSuccess = await _orderRepository.UpdateOrderStatusInOrder(order);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        await _notificationSender.SendNotificationWithOrderToUser(order);
        return order;
    }
}