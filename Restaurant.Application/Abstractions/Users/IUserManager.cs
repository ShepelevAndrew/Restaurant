using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Abstractions.Users;

public interface IUserManager : IUserRepository
{
    Task<string> GenerateCode(User user);

    Task<bool> ConfirmEmail(User user, string code);
}