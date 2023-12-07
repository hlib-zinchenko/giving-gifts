using FluentValidation;
using GivingGifts.Wishlists.UseCases.CreateWishes;
using GivingGifts.Wishlists.Core.DTO.Validators;

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