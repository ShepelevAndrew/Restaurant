using ErrorOr;
using MediatR;

namespace Restaurant.Application.Users.Delete;

public record DeleteUserCommand(
    string Email) : IRequest<ErrorOr<bool>>;