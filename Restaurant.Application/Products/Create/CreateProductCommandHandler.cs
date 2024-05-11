using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Products.Create;

public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, ErrorOr<Product>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price, request.Weight, request.Description, request.CategoryId);
        if (await _productRepository.IsAliasExist(product.Alias))
        {
            return Errors.Product.DuplicateAlias;
        }

        var isSuccess = await _productRepository.Create(product);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return product;
    }
}