using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;

namespace Restaurant.Application.Orders.Cooked;

public record CookedOrderCommand(Guid OrderId) : IRequest<ErrorOr<Order>>;