using ErrorOr;
using MediatR;
using Restaurant.Domain.Users.Entities;

namespace Restaurant.Application.Roles.GetOneById;

public record GetRoleByIdQuery(
    int RoleId) : IRequest<ErrorOr<Role>>;