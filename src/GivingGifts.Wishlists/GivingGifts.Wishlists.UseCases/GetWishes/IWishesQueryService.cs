using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.DTO;

namespace GivingGifts.Wishlists.UseCases.GetWishes;

public interface IWishesQueryService
{
    Task<PagedData<WishDto>> GetWishesAsync(
        Guid userId,
        Guid wishlistId,
        int page,
        int pageSize,
        SortingParameter[] sortingParams);
}