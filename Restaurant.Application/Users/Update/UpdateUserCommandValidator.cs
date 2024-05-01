using FluentValidation;

namespace Restaurant.Application.Users.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Phone)
            .MinimumLength(7)
            .WithMessage("PhoneNumber must not be less than 7 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required.")
            .EmailAddress()
            .WithMessage("It's not valid email address.");
    }
}