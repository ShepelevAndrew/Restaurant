using ErrorOr;
using MediatR;
using Restaurant.Domain.Products;

namespace Restaurant.Application.Products.AddRating;

public record AddRatingCommand(
    string Alias,
    int Mark) : IRequest<ErrorOr<Product>>;