namespace Restaurant.Domain.Orders.Enums;

public enum OrderStatus
{
    Open,
    Checkout,
    Confirmed,
    Cooked,
    Shipped,
    Paid,
    Cancelled,
}