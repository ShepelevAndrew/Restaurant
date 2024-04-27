namespace Restaurant.Domain.Users.Repositories;

public interface IUserRepository
{
    Task Create(User user);

    Task<User?> GetByEmailAsync(string email);

    Task<bool> IsEmailExist(string email);
}