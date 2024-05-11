using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;

namespace Restaurant.Application.Products.AddToCart;

public record AddToCartCommand(
    string Alias,
    int Quantity,
    Guid UserId) : IRequest<ErrorOr<Order>>;
