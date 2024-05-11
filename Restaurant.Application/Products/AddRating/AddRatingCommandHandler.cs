using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Repositories;
using Restaurant.Domain.Products.ValueObjects;

namespace Restaurant.Application.Products.AddRating;

public class AddRatingCommandHandler
    : IRequestHandler<AddRatingCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public AddRatingCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(AddRatingCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByAlias(request.Alias);
        if (product is null)
        {
            return Errors.Product.ProductNotFound;
        }

        var updatedProduct = product.AddRating(request.Mark);

        var isSuccess = await _productRepository.Update(updatedProduct);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return updatedProduct;
    }
}