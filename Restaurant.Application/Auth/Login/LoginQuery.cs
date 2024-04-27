using ErrorOr;
using MediatR;
using Restaurant.Application.Auth.Common;

namespace Restaurant.Application.Auth.Login;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;