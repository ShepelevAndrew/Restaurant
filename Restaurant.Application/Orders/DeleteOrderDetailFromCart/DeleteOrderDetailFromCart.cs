using ErrorOr;
using MediatR;

namespace Restaurant.Application.Orders.DeleteOrderDetailFromCart;

public record DeleteOrderDetailFromCart(
    string Alias,
    Guid UserId) : IRequest<ErrorOr<bool>>;