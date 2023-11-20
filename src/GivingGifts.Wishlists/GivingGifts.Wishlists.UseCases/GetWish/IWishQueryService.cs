namespace GivingGifts.Wishlists.UseCases.GetWish;

public interface IWishQueryService
{
    Task<UserWishDto?> GetUserWish(Guid wishId);
}