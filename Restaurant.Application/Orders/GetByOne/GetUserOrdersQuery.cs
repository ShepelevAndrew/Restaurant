using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;

namespace Restaurant.Application.Orders.GetByOne;

public record GetUserOrdersQuery(
    Guid UserId) : IRequest<ErrorOr<List<Order>>>;