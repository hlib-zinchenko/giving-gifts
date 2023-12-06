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

        RuleForEach(c => c.Wishes)
            .SetValidator(_ => new CreateWishDtoValidator());
    }

    private class CreateWishDtoValidator : AbstractValidator<CreateWishDto>
    {
        public CreateWishDtoValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(PropertyLengthLimitation.Medium)
                .NotEmpty();

            RuleFor(c => c.Url)
                .MaximumLength(PropertyLengthLimitation.Giant);
        }
    }
}