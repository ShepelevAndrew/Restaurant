using Restaurant.Domain.Users;

namespace Restaurant.Application.Common.Abstractions.Auth;

public interface IJwtProvider
{
    string Generate(User user);
}