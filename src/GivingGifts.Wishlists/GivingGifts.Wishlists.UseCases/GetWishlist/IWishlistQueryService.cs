namespace GivingGifts.Wishlists.UseCases.GetWishlist;

public interface IWishlistQueryService
{
    Task<WishlistWithWishesDto?> GetWishlist(Guid wishlistId);
}