using ErrorOr;
using MediatR;
using Restaurant.Domain.Products.Entities;

namespace Restaurant.Application.Categories.Get;

public record GetCategoriesQuery : IRequest<ErrorOr<List<Category>>>;