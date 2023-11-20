namespace GivingGifts.Wishlists.UseCases.GetWishes;

public interface IWishesQueryService
{
    Task<IEnumerable<UserWishDto>> GetWishesAsync(Guid wishlistId);
}