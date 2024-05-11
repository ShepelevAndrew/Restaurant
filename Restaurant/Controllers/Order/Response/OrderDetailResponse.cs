namespace Restaurant.Controllers.Order.Response;

public record OrderDetailResponse(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal Price);