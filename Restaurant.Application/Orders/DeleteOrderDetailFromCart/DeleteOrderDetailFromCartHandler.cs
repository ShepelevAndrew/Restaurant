using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Orders.Repositories;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Orders.DeleteOrderDetailFromCart;

public class DeleteOrderDetailFromCartHandler
    : IRequestHandler<DeleteOrderDetailFromCart, ErrorOr<bool>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;

    public DeleteOrderDetailFromCartHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IOrderDetailRepository orderDetailRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteOrderDetailFromCart request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderFromCart(request.UserId);
        if (order is null)
        {
            return Errors.Order.OrderNotFound;
        }

        var product = await _productRepository.GetByAlias(request.Alias);
        if (product is null)
        {
            return Errors.Product.ProductNotFound;
        }

        var orderDetail = order.OrderDetails.FirstOrDefault(od => od.ProductId == product.ProductId);
        if (orderDetail is null)
        {
            return Errors.Product.ProductNotFound;
        }

        var isSuccess = await _orderDetailRepository.Delete(orderDetail.OrderDetailId);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return true;
    }
}