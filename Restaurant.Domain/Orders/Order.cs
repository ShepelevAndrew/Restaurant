using Restaurant.Domain.Orders.Entities;
using Restaurant.Domain.Orders.Enums;
using Restaurant.Domain.Products;

namespace Restaurant.Domain.Orders;

public class Order
{
    private readonly List<OrderDetail> _orderDetails = new();

    public Order(Guid userId)
    {
        OrderId = Guid.NewGuid();
        Sum = 0;
        Location = string.Empty;
        BuyDate = null;
        ConfirmedDate = null;
        CookedDate = null;
        ShippedDate = null;
        PaidDate = null;
        CancelledDate = null;
        Status = OrderStatus.Open;
        UserId = userId;
    }

    public Order(Guid userId, string location)
        : this(userId)
    {
        Location = location;
    }

    // For EF Core
    private Order()
    {
    }

    public Guid OrderId { get; private set; }

    public decimal Sum { get; private set; }

    public string Location { get; private set; } = string.Empty;

    public DateTime? BuyDate { get; private set; }

    public DateTime? ConfirmedDate { get; private set; }

    public DateTime? CookedDate { get; private set; }

    public DateTime? ShippedDate { get; private set; }

    public DateTime? PaidDate { get; private set; }

    public DateTime? CancelledDate { get; private set; }

    public OrderStatus Status { get; private set; }

    public Guid UserId { get; private set; }

    public IReadOnlyList<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

    public void ChangeOrderStatus(OrderStatus status)
    {
        var currentDate = DateTime.UtcNow;

        switch (status)
        {
            case OrderStatus.Open:
                break;
            case OrderStatus.Checkout:
                BuyDate = currentDate;
                break;
            case OrderStatus.Confirmed:
                ConfirmedDate = currentDate;
                break;
            case OrderStatus.Cooked:
                CookedDate = currentDate;
                break;
            case OrderStatus.Shipped:
                ShippedDate = currentDate;
                break;
            case OrderStatus.Paid:
                PaidDate = currentDate;
                break;
            case OrderStatus.Cancelled:
                CancelledDate = currentDate;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Status = status;
    }

    public void AddOrderDetail(Product product, int quantity)
    {
        var orderDetail = new OrderDetail(OrderId, product, quantity);

        AddToSum(orderDetail.Price);
        _orderDetails.Add(orderDetail);
    }

    private void AddToSum(decimal price)
    {
        if (price < 0)
        {
            throw new ArgumentException("Invalid price number.", nameof(price));
        }

        Sum += price;
    }

    public void UpdateOrderInfo(string location)
    {
        Location = location;
    }
}