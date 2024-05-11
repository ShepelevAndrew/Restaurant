using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders.Entities;

namespace Restaurant.Application.Orders.GetOrderDetailsFromCart;

public record GetOrderDetailsFromCart(
    Guid UserId) : IRequest<ErrorOr<List<OrderDetail>>>;