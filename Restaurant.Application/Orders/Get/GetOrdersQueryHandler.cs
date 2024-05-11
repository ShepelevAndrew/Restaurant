using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Enums;
using Restaurant.Domain.Orders.Repositories;

namespace Restaurant.Application.Orders.Get;

public class GetQueryHandler
    : IRequestHandler<GetOrdersQuery, ErrorOr<List<Order>>>
{
    private readonly IOrderRepository _orderRepository;

    public GetQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<List<Order>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersWithStatus(request.Status);
        return orders.ToList();
    }
}