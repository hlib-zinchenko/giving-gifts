using FluentValidation;

namespace GivingGifts.Wishlists.UseCases.GetWish;

public class WishQueryValidator : AbstractValidator<WishQuery>
{
    public WishQueryValidator()
    {
        RuleFor(q => q.WishId)
            .NotEmpty();
    }
}