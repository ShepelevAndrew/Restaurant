using ErrorOr;
using MediatR;
using Restaurant.Application.Common.Abstractions.NotificationSenders;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Enums;
using Restaurant.Domain.Orders.Repositories;

namespace Restaurant.Application.Orders.Verification;

public class VerificationOrderCommandHandler
    : IRequestHandler<VerificationOrderCommand, ErrorOr<Order>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly INotificationSender _notificationSender;

    public VerificationOrderCommandHandler(
        IOrderRepository orderRepository,
        INotificationSender notificationSender)
    {
        _orderRepository = orderRepository;
        _notificationSender = notificationSender;
    }

    public async Task<ErrorOr<Order>> Handle(VerificationOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrder(request.OrderId);
        if (order is null)
        {
            return Errors.Order.OrderNotFound;
        }

        order.ChangeOrderStatus(OrderStatus.Confirmed);

        var isSuccess = await _orderRepository.UpdateOrderStatusInOrder(order);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        await _notificationSender.SendNotificationWithOrderToUser(order);
        await _notificationSender.SendNotificationWithOrderToCouriers(order);

        return order;
    }
}