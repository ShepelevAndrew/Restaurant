using Restaurant.Domain.Products;

namespace Restaurant.Domain.Orders.Entities;

public class OrderDetail
{
    public OrderDetail(Guid orderId, Product product, int quantity)
    {
        OrderDetailId = Guid.NewGuid();
        OrderId = orderId;
        ProductId = product.ProductId;
        ProductName = product.Name;
        Quantity = quantity;
        Price = product.Price * quantity;
    }

    // For EF Core
    private OrderDetail()
    {
    }

    public Guid OrderDetailId { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; } = string.Empty;

    public int Quantity { get; private set; }

    public decimal Price { get; private set; }
}