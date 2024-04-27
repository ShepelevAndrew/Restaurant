using Restaurant.Domain.Users;

namespace Restaurant.Application.Abstractions.Auth;

public interface IJwtProvider
{
    string Generate(User user);
}