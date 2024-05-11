using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Users.Entities;
using Restaurant.Infrastructure.Persistent.ModelConfiguration.Entities;

namespace Restaurant.Infrastructure.Persistent.ModelConfiguration;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.HasData(Create(
            Role.Owner,
            Role.Admin,
            Role.Manager,
            Role.Operator,
            Role.Courier,
            Role.User));
    }

    private static IEnumerable<RolePermission> Create(params Role[] roles)
    {
        var rolePermissions = new List<RolePermission>();
        foreach (var role in roles)
        {
            rolePermissions.AddRange(Create(role));
        }

        return rolePermissions;
    }

    private static IEnumerable<RolePermission> Create(Role role)
    {
        return role.Permissions.Select(permission => new RolePermission(role.Id, permission.Id));
    }
}