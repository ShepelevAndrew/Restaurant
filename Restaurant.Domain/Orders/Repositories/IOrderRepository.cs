using Restaurant.Domain.Orders.Entities;
using Restaurant.Domain.Orders.Enums;

namespace Restaurant.Domain.Orders.Repositories;

public interface IOrderRepository
{
    Task<bool> Create(Order order);

    Task<Order?> GetOrder(Guid orderId);

    Task<Order?> GetOrderFromCart(Guid userId);

    Task<IEnumerable<Order>> GetOrdersByUserId(Guid userId);

    Task<IEnumerable<Order>> GetOrdersWithStatus(OrderStatus status);

    Task<bool> UpdateOrderStatusInOrder(Order order);

    Task<bool> UpdateOrderInfoInOrder(Order order);

    Task<bool> UpdateOrderDetailsInOrder(Order order);
}