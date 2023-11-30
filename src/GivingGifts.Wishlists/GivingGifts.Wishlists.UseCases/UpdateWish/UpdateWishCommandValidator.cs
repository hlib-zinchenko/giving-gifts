using FluentValidation;
using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.Wishlists.UseCases.UpdateWish;

public class UpdateWishCommandValidator : AbstractValidator<UpdateWishCommand>
{
    public UpdateWishCommandValidator()
    {
        RuleFor(c => c.WishlistId)
            .NotEmpty();

        RuleFor(c => c.WishId)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(PropertyLengthLimitation.Medium);
    }
}