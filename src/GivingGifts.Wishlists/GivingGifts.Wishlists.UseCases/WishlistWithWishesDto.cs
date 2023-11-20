namespace GivingGifts.Wishlists.UseCases;

public record WishlistWithWishesDto(Guid UserId, Guid Id, string Name, WishDto[] Wishes);