namespace Restaurant.Controllers.Product.Request;

public record ProductRequest(
    string Name,
    decimal Price,
    uint Weight,
    string Description,
    Guid CategoryId);