using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;

namespace Restaurant.Application.Orders.Paid;

public record PaidOrderCommand(Guid OrderId) : IRequest<ErrorOr<Order>>;