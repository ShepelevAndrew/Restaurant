using ErrorOr;
using MediatR;
using Restaurant.Domain.Products;

namespace Restaurant.Application.Products.Update;

public record UpdateProductCommand(
    string Alias,
    string Name,
    decimal Price,
    uint Weight,
    string Description,
    Guid CategoryId) : IRequest<ErrorOr<Product>>;