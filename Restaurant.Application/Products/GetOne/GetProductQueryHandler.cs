using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Products.GetOne;

public class GetProductQueryHandler
    : IRequestHandler<GetProductQuery, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByAlias(request.Alias);
        if (product is null)
        {
            return Errors.Product.ProductNotFound;
        }

        return product;
    }
}