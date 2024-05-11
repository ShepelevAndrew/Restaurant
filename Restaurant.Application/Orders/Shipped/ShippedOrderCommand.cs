using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;

namespace Restaurant.Application.Orders.Shipped;

public record ShippedOrderCommand(Guid OrderId) : IRequest<ErrorOr<Order>>;