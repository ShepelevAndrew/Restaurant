using ErrorOr;
using MediatR;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Repositories;

namespace Restaurant.Application.Orders.GetByOne;

public class GetUserOrdersQueryHandler
    : IRequestHandler<GetUserOrdersQuery, ErrorOr<List<Order>>>
{
    private readonly IOrderRepository _orderRepository;

    public GetUserOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<List<Order>>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByUserId(request.UserId);
        return orders.ToList();
    }
}