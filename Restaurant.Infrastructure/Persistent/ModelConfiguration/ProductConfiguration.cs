using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Entities;

namespace Restaurant.Infrastructure.Persistent.ModelConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductId);

        builder.OwnsOne(p => p.AverageRating);

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId);
    }
}