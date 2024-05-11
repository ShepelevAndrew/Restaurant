using Restaurant.Domain.Users.Entities;

namespace Restaurant.Domain.Users.Repositories;

public interface IPermissionRepository
{
    Task<IEnumerable<Permission>> Get();

    Task<IEnumerable<Permission>> GetPermissionsByIds(params int[] permissionIds);

    Task<HashSet<string>> GetPermissionNames(Guid userId);
}