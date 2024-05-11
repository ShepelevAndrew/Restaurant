using ErrorOr;
using MediatR;
using Restaurant.Domain.Products;

namespace Restaurant.Application.Products.Create;

public record CreateProductCommand(
    string Name,
    decimal Price,
    uint Weight,
    string Description,
    Guid CategoryId) : IRequest<ErrorOr<Product>>;