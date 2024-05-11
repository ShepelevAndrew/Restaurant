using ErrorOr;
using MediatR;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Products.Get;

public class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, ErrorOr<List<Product>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.Get();
        return products.ToList();
    }
}