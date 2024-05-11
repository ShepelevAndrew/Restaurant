using Restaurant.Domain.Orders;

namespace Restaurant.Application.Common.Abstractions.NotificationSenders;

public interface INotificationSender
{
    Task SendNotificationWithOrderToOperators(Order order);

    Task SendNotificationWithOrderToCouriers(Order order);

    Task SendNotificationWithOrderToUser(Order order);

    Task SendNotificationWithOrderAndDescriptionToUser(Order order, string description);
}