using ErrorOr;
using MediatR;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Users.Get;

public class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, ErrorOr<List<User>>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.Get();
        return users.ToList();
    }
}