using ErrorOr;
using MediatR;
using Restaurant.Domain.Users;

namespace Restaurant.Application.Users.ChangeUserRole;

public record ChangeUserRoleCommand(
    string Email,
    int RoleId) : IRequest<ErrorOr<User>>;