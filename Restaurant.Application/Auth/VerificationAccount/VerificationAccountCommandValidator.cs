using FluentValidation;

namespace Restaurant.Application.Auth.VerificationAccount;

public class VerificationAccountCommandValidator : AbstractValidator<VerificationAccountCommand>
{
    public VerificationAccountCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Verification code is required.")
            .MinimumLength(8)
            .WithMessage("Minimum length code is 8 digits.")
            .MaximumLength(8)
            .WithMessage("Maximum length code is 8 digits.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required.")
            .EmailAddress()
            .WithMessage("It's not valid email address.");
    }
}