using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Users.GetOne;

public class GetUserQueryHandler
    : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            return Errors.User.UserNotFound;
        }

        return user;
    }
}