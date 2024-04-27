using ErrorOr;
using MediatR;
using Restaurant.Application.Abstractions.Auth;
using Restaurant.Application.Auth.Common;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Auth.Register;

public sealed class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Firstname, request.Lastname, request.Email, request.Phone, request.Password);

        if (await _userRepository.IsEmailExist(user.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        await _userRepository.Create(user);
        var token = _jwtProvider.Generate(user);

        return new AuthenticationResult(user, token);
    }
}