using FluentValidation;

namespace Restaurant.Application.Auth.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address can't be empty.")
            .EmailAddress()
            .WithMessage("It's not valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password can't be empty.");
    }
}