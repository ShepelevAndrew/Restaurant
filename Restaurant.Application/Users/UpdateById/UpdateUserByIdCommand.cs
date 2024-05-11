using ErrorOr;
using MediatR;
using Restaurant.Domain.Users;

namespace Restaurant.Application.Users.UpdateById;

public record UpdateUserByIdCommand(
    Guid UserId,
    string? Firstname,
    string? Lastname,
    string? Phone) : IRequest<ErrorOr<User>>;