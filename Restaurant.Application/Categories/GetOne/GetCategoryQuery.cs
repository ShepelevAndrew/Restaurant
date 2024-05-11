using ErrorOr;
using MediatR;
using Restaurant.Domain.Products.Entities;

namespace Restaurant.Application.Categories.GetOne;

public record GetCategoryQuery(
    string Alias) : IRequest<ErrorOr<Category>>;