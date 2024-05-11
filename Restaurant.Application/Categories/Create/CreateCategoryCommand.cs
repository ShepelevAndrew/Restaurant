using ErrorOr;
using MediatR;
using Restaurant.Domain.Products.Entities;

namespace Restaurant.Application.Categories.Create;

public record CreateCategoryCommand(
    string Name,
    string Description,
    Guid? ParentId) : IRequest<ErrorOr<Category>>;