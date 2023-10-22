using FluentValidation;

namespace GivingGifts.Wishlists.UseCases.Delete;

public class DeleteWishlistCommandValidator : AbstractValidator<DeleteWishlistCommand>
{
    public DeleteWishlistCommandValidator()
    {
        RuleFor(c => c.WishlistId)
            .NotEmpty();
    }
}