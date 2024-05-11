using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users.Entities;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Roles.Create;

public class CreateRoleCommandHandler
    : IRequestHandler<CreateRoleCommand, ErrorOr<Role>>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public CreateRoleCommandHandler(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task<ErrorOr<Role>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var permissions = await _permissionRepository.GetPermissionsByIds(request.PermissionIds.ToArray());
        var roleId = await _roleRepository.GenerateId();

        var role = new Role(roleId, request.Name);
        role.AddPermission(permissions.ToArray());

        var isSuccess = await _roleRepository.Create(role);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return role;
    }
}