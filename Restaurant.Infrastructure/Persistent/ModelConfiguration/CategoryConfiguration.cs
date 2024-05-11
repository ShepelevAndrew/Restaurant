using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Products.Entities;

namespace Restaurant.Infrastructure.Persistent.ModelConfiguration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.CategoryId);

        builder.HasOne<Category>()
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentId)
            .IsRequired(false);
    }
}