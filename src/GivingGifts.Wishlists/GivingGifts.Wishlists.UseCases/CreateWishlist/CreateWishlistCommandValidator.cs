using FluentValidation;
using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.Wishlists.Core.DTO.Validators;

namespace GivingGifts.Wishlists.UseCases.CreateWishlist;

public class CreateWishlistCommandValidator : AbstractValidator<CreateWishlistCommand>
{
    public CreateWishlistCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(PropertyLengthLimitation.Medium);

        RuleForEach(c => c.Wishes)
            .SetValidator(_ => new CreateWishDtoValidator());
    }
}