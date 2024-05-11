using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Users.Update;

public class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user is null)
        {
            return Errors.User.UserNotFound;
        }

        var updatedUser = user.Update(request.Firstname, request.Lastname, request.Phone);
        var isSuccess = await _userRepository.Update(updatedUser);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return updatedUser;
    }
}