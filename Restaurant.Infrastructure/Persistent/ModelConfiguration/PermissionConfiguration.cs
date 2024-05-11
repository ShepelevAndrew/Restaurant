using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Users.Entities;

namespace Restaurant.Infrastructure.Persistent.ModelConfiguration;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);

        var permissions = Enum.GetValues<Domain.Users.Enums.Permissions>()
            .Select(p => new Permission((int)p, p.ToString()));

        builder.HasData(permissions);
    }
}