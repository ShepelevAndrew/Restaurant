using ErrorOr;
using MediatR;
using Restaurant.Domain.Users;

namespace Restaurant.Application.Users.Update;

public record UpdateUserCommand(
    string? Firstname,
    string? Lastname,
    string Email,
    string? Phone) : IRequest<ErrorOr<User>>;