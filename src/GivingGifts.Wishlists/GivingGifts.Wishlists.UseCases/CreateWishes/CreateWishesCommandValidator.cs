using FluentValidation;
using GivingGifts.Wishlists.Core.DTO.Validators;

namespace GivingGifts.Wishlists.UseCases.CreateWishes;

public class CreateWishesCommandValidator
    : AbstractValidator<CreateWishesCommand>
{
    public CreateWishesCommandValidator()
    {
        RuleFor(x => x.WishlistId)
            .NotEmpty()
            .WithMessage("WishlistId is required.");

        RuleForEach(x => x.Wishes).SetValidator(new CreateWishDtoValidator());
    }
}