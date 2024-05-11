using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;

namespace Restaurant.Application.Payment.Buy;

public record BuyCommand(Guid UserId) : IRequest<ErrorOr<Order>>;