using FluentValidation;
using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.Wishlists.UseCases.CreateWishlist;

public class CreateWishlistCommandValidator : AbstractValidator<CreateWishlistCommand>
{
    public CreateWishlistCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(PropertyLengthLimitation.Medium);
    }
}