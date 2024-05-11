namespace Restaurant.Controllers.Role.Request;

public record RoleRequest(
    string Name,
    IEnumerable<int> PermissionIds);