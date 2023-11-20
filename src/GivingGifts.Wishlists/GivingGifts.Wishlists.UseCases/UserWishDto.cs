namespace GivingGifts.Wishlists.UseCases;

public record UserWishDto(Guid UserId, Guid WishlistId, Guid Id, string Name, string? Url);