using Restaurant.Controllers.Permission.Response;

namespace Restaurant.Controllers.Role.Response;

public record RoleResponse(
    int Id,
    string Name,
    IEnumerable<PermissionResponse> Permissions);