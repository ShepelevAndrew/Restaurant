using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Users.UpdateById;

public class UpdateUserByIdCommandHandler
    : IRequestHandler<UpdateUserByIdCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserByIdCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId);
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