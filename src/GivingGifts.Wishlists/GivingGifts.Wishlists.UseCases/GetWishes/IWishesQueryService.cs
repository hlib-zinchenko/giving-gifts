using GivingGifts.SharedKernel.Core;

namespace GivingGifts.Wishlists.UseCases.GetWishes;

public interface IWishesQueryService
{
    Task<PagedData<UserWishDto>> GetWishesAsync(
        Guid userId,
        Guid wishlistId,
        int page,
        int pageSize);
}