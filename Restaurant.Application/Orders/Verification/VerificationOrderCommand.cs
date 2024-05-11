using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;

namespace Restaurant.Application.Orders.Verification;

public record VerificationOrderCommand(Guid OrderId) : IRequest<ErrorOr<Order>>;