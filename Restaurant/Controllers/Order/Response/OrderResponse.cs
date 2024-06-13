namespace Restaurant.Controllers.Order.Response;

public record OrderResponse(
    Guid OrderId,
    decimal Sum,
    string Status,
    string Location,
    DateTime? BuyDate,
    DateTime? ConfirmedDate,
    DateTime? CookedDate,
    DateTime? ShippedDate,
    DateTime? PaidDate,
    DateTime? CancelledDate,
    Guid UserId,
    IEnumerable<OrderDetailResponse> OrderDetails);