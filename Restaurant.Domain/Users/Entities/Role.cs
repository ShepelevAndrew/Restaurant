using Restaurant.Domain.Users.Enums;

namespace Restaurant.Domain.Users.Entities;

public class Role
{
    public static readonly Role Owner = new((int)Roles.Owner, Roles.Owner.ToString());
    public static readonly Role Admin = new((int)Roles.Admin, Roles.Admin.ToString());
    public static readonly Role Manager = new((int)Roles.Manager, Roles.Manager.ToString());
    public static readonly Role Operator = new((int)Roles.Operator, Roles.Operator.ToString());
    public static readonly Role Courier = new((int)Roles.Courier, Roles.Courier.ToString());
    public static readonly Role User = new((int)Roles.User, Roles.User.ToString());

    static Role()
    {
        Owner.AddPermission(
            Enums.Permissions.Create,
            Enums.Permissions.Read,
            Enums.Permissions.Update,
            Enums.Permissions.Delete,
            Enums.Permissions.GetPaidOrders,
            Enums.Permissions.GetCancelledOrders,
            Enums.Permissions.ReadUsers,
            Enums.Permissions.UpdateMyUser,
            Enums.Permissions.UpdateUsers,
            Enums.Permissions.DeleteUsers,
            Enums.Permissions.CreateRole);

        Admin.AddPermission(
            Enums.Permissions.Create,
            Enums.Permissions.Read,
            Enums.Permissions.Update,
            Enums.Permissions.Delete,
            Enums.Permissions.GetPaidOrders,
            Enums.Permissions.GetCancelledOrders,
            Enums.Permissions.ReadUsers,
            Enums.Permissions.UpdateMyUser,
            Enums.Permissions.UpdateUsers);

        Manager.AddPermission(
            Enums.Permissions.Create,
            Enums.Permissions.Read,
            Enums.Permissions.Update,
            Enums.Permissions.ReadUsers,
            Enums.Permissions.UpdateMyUser);

        Operator.AddPermission(
            Enums.Permissions.Read,
            Enums.Permissions.GetBoughtOrders,
            Enums.Permissions.VerifyOrder,
            Enums.Permissions.CancelledOrder,
            Enums.Permissions.ReadUsers,
            Enums.Permissions.UpdateMyUser);

        Courier.AddPermission(
            Enums.Permissions.Read,
            Enums.Permissions.GetVerifiedOrders,
            Enums.Permissions.ShippedOrder,
            Enums.Permissions.PaidOrder,
            Enums.Permissions.CancelledOrder,
            Enums.Permissions.UpdateMyUser);

        User.AddPermission(
            Enums.Permissions.Read,
            Enums.Permissions.Cart,
            Enums.Permissions.BuyOrder,
            Enums.Permissions.UpdateMyUser);
    }

    private readonly List<Permission> _permissions = new();

    public Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public IReadOnlyList<Permission> Permissions => _permissions.AsReadOnly();

    public void AddPermission(params Permission[] permissions)
    {
        foreach (var permission in permissions)
        {
            _permissions.Add(permission);
        }
    }

    public void AddPermission(params Permissions[] permissions)
    {
        foreach (var permission in permissions)
        {
            var createdPermission = new Permission((int)permission, permission.ToString());
            _permissions.Add(createdPermission);
        }
    }
}