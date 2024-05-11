using ErrorOr;
using MediatR;
using Restaurant.Application.Common.Abstractions.NotificationSenders;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Enums;
using Restaurant.Domain.Orders.Repositories;

namespace Restaurant.Application.Orders.Cancelled;

public class CancelledOrderCommandHandler
    : IRequestHandler<CancelledOrderCommand, ErrorOr<Order>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly INotificationSender _notificationSender;

    public CancelledOrderCommandHandler(
        IOrderRepository orderRepository,
        INotificationSender notificationSender)
    {
        _orderRepository = orderRepository;
        _notificationSender = notificationSender;
    }

    public async Task<ErrorOr<Order>> Handle(CancelledOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(request.OrderId);
        if (order is null)
        {
            return Errors.Order.OrderNotFound;
        }

        order.ChangeOrderStatus(OrderStatus.Cancelled);

        var isSuccess = await _orderRepository.UpdateOrderStatusInOrder(order);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        await _notificationSender.SendNotificationWithOrderAndDescriptionToUser(order, request.Description);
        return order;
    }
}