namespace Restaurant.Infrastructure.Persistent.ModelConfiguration.Entities;

public class RolePermission
{
    public RolePermission(int roleId, int permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public int RoleId { get; private set; }

    public int PermissionId { get; private set; }
}