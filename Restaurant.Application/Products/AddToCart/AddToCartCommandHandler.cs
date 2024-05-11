using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Repositories;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Products.AddToCart;

public class AddToCartCommandHandler
    : IRequestHandler<AddToCartCommand, ErrorOr<Order>>
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public AddToCartCommandHandler(IProductRepository productRepository, IOrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<Order>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByAlias(request.Alias);
        if (product is null)
        {
            return Errors.Product.ProductNotFound;
        }

        var order = await _orderRepository.GetOrderFromCart(request.UserId);
        if (order is null)
        {
            return await CreateOrderWithProduct(request.UserId, product, request.Quantity);
        }

        return await AddProductToOrder(order, product, request.Quantity);
    }

    private async Task<ErrorOr<Order>> CreateOrderWithProduct(Guid userId, Product product, int quantity)
    {
        var order = new Order(userId);
        order.AddOrderDetail(product, quantity);

        var isSuccess = await _orderRepository.Create(order);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return order;
    }

    private async Task<ErrorOr<Order>> AddProductToOrder(Order order, Product product, int quantity)
    {
        order.AddOrderDetail(product, quantity);

        var isSuccess = await _orderRepository.UpdateOrderDetailsInOrder(order);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return order;
    }
}