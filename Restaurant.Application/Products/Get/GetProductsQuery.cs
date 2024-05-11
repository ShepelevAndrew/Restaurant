using ErrorOr;
using MediatR;
using Restaurant.Domain.Products;

namespace Restaurant.Application.Products.Get;

public record GetProductsQuery : IRequest<ErrorOr<List<Product>>>;