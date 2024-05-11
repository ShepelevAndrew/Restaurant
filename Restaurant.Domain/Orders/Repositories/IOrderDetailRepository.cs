using Restaurant.Domain.Orders.Entities;

namespace Restaurant.Domain.Orders.Repositories;

public interface IOrderDetailRepository
{
    Task<bool> Create(OrderDetail orderDetail);

    Task<bool> Create(IEnumerable<OrderDetail> orderDetails);

    Task<OrderDetail?> GetById(Guid orderDetailId);

    Task<bool> Delete(Guid orderDetailId);
}