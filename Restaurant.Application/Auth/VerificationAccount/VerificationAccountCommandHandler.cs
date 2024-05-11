using ErrorOr;
using MediatR;
using Restaurant.Application.Common.Abstractions.Auth;
using Restaurant.Application.Common.Abstractions.Users;
using Restaurant.Application.Auth.Common;
using Restaurant.Domain.Errors;

namespace Restaurant.Application.Auth.VerificationAccount;

public class VerificationAccountCommandHandler
    : IRequestHandler<VerificationAccountCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserManager _userManager;
    private readonly IJwtProvider _jwtProvider;

    public VerificationAccountCommandHandler(IUserManager userManager, IJwtProvider jwtProvider)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(VerificationAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetByEmail(request.Email);
        if (user is null)
        {
            return Errors.User.UserNotFound;
        }

        var result = await _userManager.ConfirmEmail(user, request.Code);
        if (result is false)
        {
            return Errors.Authentication.VerificationCodeInvalid;
        }

        var token = _jwtProvider.Generate(user);
        return new AuthenticationResult(user, token);
    }
}