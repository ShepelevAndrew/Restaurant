using ErrorOr;
using MediatR;
using Restaurant.Domain.Products;

namespace Restaurant.Application.ProductsCategory.GetProducts;

public record GetProductsByCategory(
    string CategoryAlias) : IRequest<ErrorOr<List<Product>>>;