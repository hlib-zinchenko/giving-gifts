namespace GivingGifts.Wishlists.UseCases.GetUserWishlists;

public interface IUserWishlistsQueryService
{
    Task<IEnumerable<WishlistDto>> UserWishlistsAsync(Guid userId);
}