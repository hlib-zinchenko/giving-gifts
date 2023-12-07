namespace GivingGifts.Wishlists.Core.DTO;

public record WishlistWithWishesDto(Guid UserId, Guid Id, string Name, WishDto[] Wishes);