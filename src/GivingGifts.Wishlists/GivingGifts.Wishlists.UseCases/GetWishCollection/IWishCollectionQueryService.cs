namespace GivingGifts.Wishlists.UseCases.GetWishCollection;

public interface IWishCollectionQueryService
{
    Task<IEnumerable<UserWishDto>> GetWishesAsync(Guid wishlistId, Guid[] wishIds);
}