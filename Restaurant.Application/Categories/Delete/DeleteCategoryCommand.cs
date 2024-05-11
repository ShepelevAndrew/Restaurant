using ErrorOr;
using MediatR;

namespace Restaurant.Application.Categories.Delete;

public record DeleteCategoryCommand(
    string Alias) : IRequest<ErrorOr<bool>>;