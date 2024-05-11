using ErrorOr;
using MediatR;
using Restaurant.Application.Common.Abstractions.NotificationSenders;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Enums;
using Restaurant.Domain.Orders.Repositories;

namespace Restaurant.Application.Payment.Buy;

public class BuyCommandHandler
    : IRequestHandler<BuyCommand, ErrorOr<Order>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly INotificationSender _notificationSender;

    public BuyCommandHandler(
        IOrderRepository orderRepository,
        INotificationSender notificationSender)
    {
        _orderRepository = orderRepository;
        _notificationSender = notificationSender;
    }

    public async Task<ErrorOr<Order>> Handle(BuyCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderFromCart(request.UserId);
        if (order is null)
        {
            return Errors.Order.OrderNotFound;
        }

        order.ChangeOrderStatus(OrderStatus.Checkout);

        var isSuccess = await _orderRepository.UpdateOrderStatusInOrder(order);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        await _notificationSender.SendNotificationWithOrderToOperators(order);
        return order;
    }
}