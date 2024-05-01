using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Users.Delete;

public class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, ErrorOr<bool>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsEmailExist(request.Email))
        {
            return Errors.User.UserNotFound;
        }

        var isSuccess = await _userRepository.Delete(request.Email);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return true;
    }
}