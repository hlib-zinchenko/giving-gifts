using FluentValidation;
using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.SharedKernel.Core.Extensions;

namespace GivingGifts.Wishlists.UseCases.CreateWish;

public class CreateWishCommandValidator : AbstractValidator<CreateWishCommand>
{
    public CreateWishCommandValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(PropertyLengthLimitation.Medium)
            .NotEmpty();

        RuleFor(c => c.WishlistId)
            .NotEmpty();

        RuleFor(c => c.Url)
            .ValidUrl()
            .MaximumLength(PropertyLengthLimitation.Giant);
    }
}