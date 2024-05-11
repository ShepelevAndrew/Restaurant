using ErrorOr;
using MediatR;
using Restaurant.Application.Users.Update;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Users.ChangeUserRole;

public class ChangeUserRoleCommandHandler
    : IRequestHandler<ChangeUserRoleCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public ChangeUserRoleCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<User>> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user is null)
        {
            return Errors.User.UserNotFound;
        }

        if (!await _roleRepository.IsExist(request.RoleId))
        {
            return Errors.Role.RoleNotFound;
        }

        var updatedUser = user.ChangeRole(request.RoleId);

        var isSuccess = await _userRepository.Update(updatedUser);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return updatedUser;
    }
}