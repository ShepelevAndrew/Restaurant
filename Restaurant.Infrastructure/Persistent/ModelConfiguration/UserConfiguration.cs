using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Entities;

namespace Restaurant.Infrastructure.Persistent.ModelConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(u => u.RoleId);

        builder.HasData(User.Owner);
    }
}