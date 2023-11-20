using FluentValidation;

namespace GivingGifts.Wishlists.UseCases.DeleteWish;

public class DeleteWishCommandValidator : AbstractValidator<DeleteWishCommand>
{
    public DeleteWishCommandValidator()
    {
        RuleFor(c => c.WishlistId)
            .NotEmpty();

        RuleFor(c => c.WishId)
            .NotEmpty();
    }
}