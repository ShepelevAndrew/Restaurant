using Restaurant.Domain.Users;

namespace Restaurant.Application.Auth.Common;

public record AuthenticationResult(
    User User,
    string Token);