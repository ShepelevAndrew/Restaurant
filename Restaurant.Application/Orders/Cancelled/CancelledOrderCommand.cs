using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;

namespace Restaurant.Application.Orders.Cancelled;

public record CancelledOrderCommand(
    Guid OrderId,
    string Description) : IRequest<ErrorOr<Order>>;