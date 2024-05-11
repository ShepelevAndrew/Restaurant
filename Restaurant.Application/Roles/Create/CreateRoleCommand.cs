using ErrorOr;
using MediatR;
using Restaurant.Domain.Users.Entities;

namespace Restaurant.Application.Roles.Create;

public record CreateRoleCommand(
    string Name,
    IEnumerable<int> PermissionIds) : IRequest<ErrorOr<Role>>;