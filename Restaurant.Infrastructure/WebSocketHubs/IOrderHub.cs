using Restaurant.Domain.Orders;

namespace Restaurant.Infrastructure.WebSocketHubs;

public interface IOrderHub
{
    Task ReceiveOrder(Order order);

    Task ReceiveCancelledOrder(Order order, string description);
}