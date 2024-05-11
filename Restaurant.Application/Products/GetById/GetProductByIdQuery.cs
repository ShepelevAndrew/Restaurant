using ErrorOr;
using MediatR;
using Restaurant.Domain.Products;

namespace Restaurant.Application.Products.GetById;

public record GetProductByIdQuery(
    Guid Id) : IRequest<ErrorOr<Product>>;