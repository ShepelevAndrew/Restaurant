using ErrorOr;
using MediatR;
using Restaurant.Domain.Products.Entities;

namespace Restaurant.Application.Categories.Update;

public record UpdateCategoryCommand(
    string Alias,
    string Name,
    string Description,
    Guid? ParentId) : IRequest<ErrorOr<Category>>;