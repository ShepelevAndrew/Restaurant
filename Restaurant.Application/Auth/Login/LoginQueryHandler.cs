using ErrorOr;
using MediatR;
using Restaurant.Application.Abstractions.Auth;
using Restaurant.Application.Auth.Common;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Auth.Login;

public sealed class LoginQueryHandler
    : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            return Errors.Authentication.EmailNotUsing;
        }
        if (!user.ComparePassword(request.Password))
        {
            return Errors.Authentication.WrongPassword;
        }

        var token = _jwtProvider.Generate(user);
        return new AuthenticationResult(user, token);
    }
}