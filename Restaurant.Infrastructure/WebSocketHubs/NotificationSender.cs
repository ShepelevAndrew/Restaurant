using Microsoft.AspNetCore.SignalR;
using Restaurant.Application.Common.Abstractions.NotificationSenders;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Users.Entities;

namespace Restaurant.Infrastructure.WebSocketHubs;

public class NotificationSender : INotificationSender
{
    private readonly IHubContext<OrderHub, IOrderHub> _hub;

    public NotificationSender(IHubContext<OrderHub, IOrderHub> hub)
    {
        _hub = hub;
    }

    public async Task SendNotificationWithOrderToOperators(Order order)
    {
        await _hub.Clients.Group(Role.Operator.Name).ReceiveOrder(order);
    }

    public async Task SendNotificationWithOrderToCouriers(Order order)
    {
        await _hub.Clients.Group(Role.Courier.Name).ReceiveOrder(order);
    }

    public async Task SendNotificationWithOrderToUser(Order order)
    {
        await _hub.Clients.User(order.UserId.ToString()).ReceiveOrder(order);
    }

    public async Task SendNotificationWithOrderAndDescriptionToUser(Order order, string description)
    {
        await _hub.Clients.User(order.UserId.ToString()).ReceiveCancelledOrder(order, description);
    }
}