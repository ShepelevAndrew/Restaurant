using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Products.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<bool>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        if (!await _productRepository.IsAliasExist(request.Alias))
        {
            return Errors.Product.ProductNotFound;
        }

        var isSuccess = await _productRepository.Delete(request.Alias);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return true;
    }
}