using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.ProductsCategory.GetProducts;

public class GetProductsByCategoryHandler
    : IRequestHandler<GetProductsByCategory, ErrorOr<List<Product>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryHandler(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<List<Product>>> Handle(GetProductsByCategory request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByAlias(request.CategoryAlias);

        if (category is null )
        {
            return Errors.Category.CategoryNotFound;
        }

        var products = await _productRepository.GetByCategory(category.CategoryId);
        return products.ToList();
    }
}