namespace Restaurant.Domain.Orders.Enums;

public enum OrderStatus
{
    Open,
    Checkout,
    Confirmed,
    Shipped,
    Paid,
    Cancelled,
}