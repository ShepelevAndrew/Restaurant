using ErrorOr;
using MediatR;

namespace Restaurant.Application.Products.Delete;

public record DeleteProductCommand(string Alias) : IRequest<ErrorOr<bool>>;