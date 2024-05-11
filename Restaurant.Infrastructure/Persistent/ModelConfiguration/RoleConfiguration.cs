using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Users.Entities;
using Restaurant.Infrastructure.Persistent.ModelConfiguration.Entities;

namespace Restaurant.Infrastructure.Persistent.ModelConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        var roles = Enum.GetValues<Domain.Users.Enums.Roles>()
            .Select(r => new Role((int)r, r.ToString()));

        builder.HasData(roles);
    }
}