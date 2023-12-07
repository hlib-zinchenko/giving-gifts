using FluentValidation;
using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.SharedKernel.Core.Extensions;

namespace GivingGifts.Wishlists.Core.DTO.Validators;

public class CreateWishDtoValidator : AbstractValidator<CreateWishDto>
{
    public CreateWishDtoValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(PropertyLengthLimitation.Medium)
            .NotEmpty();

        RuleFor(c => c.Url)
            .ValidUrl()
            .EmailAddress()
            .MaximumLength(PropertyLengthLimitation.Giant);
    }
}