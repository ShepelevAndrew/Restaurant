using ErrorOr;
using MediatR;
using Restaurant.Application.Auth.Common;

namespace Restaurant.Application.Auth.Register;

public record RegisterCommand(
    string Firstname,
    string Lastname,
    string Email,
    string Phone,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;