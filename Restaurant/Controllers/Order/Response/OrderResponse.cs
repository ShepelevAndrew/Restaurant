namespace Restaurant.Controllers.Order.Response;

public record OrderResponse(
    Guid OrderId,
    decimal Sum,
    string Status,
    DateTime? BuyDate,
    DateTime? ConfirmedDate,
    DateTime? ShippedDate,
    DateTime? PaidDate,
    DateTime? CancelledDate,
    Guid UserId,
    IEnumerable<OrderDetailResponse> OrderDetails);