using FluentValidation;

namespace Restaurant.Application.Users.UpdateById;

public class UpdateUserByIdCommandValidator : AbstractValidator<UpdateUserByIdCommand>
{
    public UpdateUserByIdCommandValidator()
    {
        RuleFor(x => x.Phone)
            .MinimumLength(7)
            .WithMessage("PhoneNumber must not be less than 7 characters.");
    }
}