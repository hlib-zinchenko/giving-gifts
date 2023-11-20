using FluentValidation;
using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.Wishlists.UseCases.UpdateWishlist;

public class UpdateWishlistCommandValidator : AbstractValidator<UpdateWishlistCommand>
{
    public UpdateWishlistCommandValidator()
    {
        RuleFor(c => c.WishlistId)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(PropertyLengthLimitation.Medium);
    }
}