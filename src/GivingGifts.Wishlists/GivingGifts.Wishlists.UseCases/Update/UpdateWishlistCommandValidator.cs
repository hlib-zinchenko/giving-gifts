using FluentValidation;
using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.Wishlists.UseCases.Create;

namespace GivingGifts.Wishlists.UseCases.Update;

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