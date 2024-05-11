namespace Restaurant.Controllers.Product.Response;

public record ProductResponse(
    Guid ProductId,
    string Alias,
    string Name,
    decimal Price,
    uint Weight,
    string Description,
    Guid CategoryId,
    double AverageRating,
    uint CountOfVoting);