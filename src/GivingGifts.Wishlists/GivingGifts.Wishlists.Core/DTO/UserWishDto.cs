namespace GivingGifts.Wishlists.Core.DTO;

public record UserWishDto(Guid UserId, Guid WishlistId, Guid Id, string Name, string? Url, string? Notes);