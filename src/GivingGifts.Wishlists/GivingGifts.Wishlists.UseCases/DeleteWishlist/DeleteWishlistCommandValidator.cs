using FluentValidation;

namespace GivingGifts.Wishlists.UseCases.DeleteWishlist;

public class DeleteWishlistCommandValidator : AbstractValidator<DeleteWishlistCommand>
{
    public DeleteWishlistCommandValidator()
    {
        RuleFor(c => c.WishlistId)
            .NotEmpty();
    }
}