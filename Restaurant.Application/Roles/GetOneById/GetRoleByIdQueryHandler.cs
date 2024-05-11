using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users.Entities;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Roles.GetOneById;

public class GetRoleByIdQueryHandler
    : IRequestHandler<GetRoleByIdQuery, ErrorOr<Role>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleByIdQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetById(request.RoleId);
        if (role is null)
        {
            return Errors.Role.RoleNotFound;
        }

        return role;
    }
}