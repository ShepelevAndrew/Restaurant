using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Enums;

namespace Restaurant.Application.Orders.Get;

public record GetOrdersQuery(
    OrderStatus Status) : IRequest<ErrorOr<List<Order>>>;