using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Products.Update;

public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByAlias(request.Alias);
        if (product is null)
        {
            return Errors.Product.ProductNotFound;
        }

        var updatedProduct = product.Update(
            request.Name,
            request.Price,
            request.Weight,
            request.Description,
            request.CategoryId);
        if (await _productRepository.IsAliasExist(updatedProduct.Alias))
        {
            return Errors.Product.DuplicateAlias;
        }

        var isSuccess = await _productRepository.Update(updatedProduct);
        if (isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return updatedProduct;
    }
}