using ErrorOr;
using MediatR;
using Restaurant.Application.Abstractions.Auth;
using Restaurant.Application.Abstractions.Users;
using Restaurant.Application.Abstractions.VerificationCode;
using Restaurant.Application.Auth.Common;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Users;

namespace Restaurant.Application.Auth.Register;

public sealed class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserManager _userManager;
    private readonly IJwtProvider _jwtProvider;
    private readonly ICodeSender _codeSender;

    public RegisterCommandHandler(IUserManager userManager, IJwtProvider jwtProvider, ICodeSender codeSender)
    {
        _userManager = userManager;
        _jwtProvider = jwtProvider;
        _codeSender = codeSender;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Firstname, request.Lastname, request.Email, request.Phone, request.Password);
        if (await _userManager.IsEmailExist(user.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        var isSuccess = await _userManager.Create(user);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        var token = _jwtProvider.Generate(user);
        var verificationCode = await _userManager.GenerateCode(user);
        await _codeSender.SendCode(user.Email, verificationCode);

        return new AuthenticationResult(user, token);
    }
}