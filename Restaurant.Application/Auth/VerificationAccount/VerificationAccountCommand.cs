using ErrorOr;
using MediatR;
using Restaurant.Application.Auth.Common;

namespace Restaurant.Application.Auth.VerificationAccount;

public record VerificationAccountCommand(
    string Email,
    string Code) : IRequest<ErrorOr<AuthenticationResult>>;