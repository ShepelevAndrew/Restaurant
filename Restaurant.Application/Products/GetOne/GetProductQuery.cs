using ErrorOr;
using MediatR;
using Restaurant.Domain.Products;

namespace Restaurant.Application.Products.GetOne;

public record GetProductQuery(
    string Alias) : IRequest<ErrorOr<Product>>;