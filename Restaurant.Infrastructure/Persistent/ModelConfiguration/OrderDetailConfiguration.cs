using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Entities;

namespace Restaurant.Infrastructure.Persistent.ModelConfiguration;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(od => od.OrderDetailId);

        builder.HasOne<Order>()
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);
    }
}