namespace GivingGifts.Wishlists.UseCases.GetWishes;

public interface IWishesQueryService
{
    Task<IEnumerable<UserWishDto>> GetWishesAsync(Guid wishlistId);
    Task<IEnumerable<UserWishDto>> GetWishesAsync(Guid wishlistId, Guid[] wishIds);
}