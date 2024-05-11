namespace Restaurant.Domain.Users.Repositories;

public interface IUserRepository
{
    Task<bool> Create(User user);

    Task<bool> Update(User user);

    Task<IEnumerable<User>> Get();

    Task<User?> GetById(Guid userId);

    Task<User?> GetByEmail(string email);

    Task<bool> Delete(string email);

    Task<bool> IsEmailExist(string email);
}