using Restaurant.Domain.Users.Entities;

namespace Restaurant.Domain.Users.Repositories;

public interface IRoleRepository
{
    Task<bool> Create(Role role);

    Task<IEnumerable<Role>> Get();

    Task<Role?> GetById(int roleId);

    Task<int> GenerateId();

    Task<bool> IsExist(int id);
}