using ErrorOr;
using MediatR;
using Restaurant.Domain.Users.Entities;

namespace Restaurant.Application.Roles.Get;

public record GetRolesQuery : IRequest<ErrorOr<List<Role>>>;