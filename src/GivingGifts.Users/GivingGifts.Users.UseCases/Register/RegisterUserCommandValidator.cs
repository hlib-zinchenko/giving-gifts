using FluentValidation;
using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.Users.UseCases.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirsName)
            .NotEmpty()
            .MaximumLength(PropertyLengthLimitation.Medium);

        RuleFor(c => c.FirsName)
            .NotEmpty()
            .MaximumLength(PropertyLengthLimitation.Medium);

        RuleFor(c => c.Email)
            .NotEmpty()
            .MaximumLength(PropertyLengthLimitation.Medium)
            .EmailAddress();

        RuleFor(c => c.Password)
            .NotEmpty();
    }
}