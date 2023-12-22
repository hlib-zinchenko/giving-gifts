using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.DTO;

namespace GivingGifts.Wishlists.UseCases.GetWishlists;

public interface IWishlistsQueryService
{
    Task<PagedData<WishlistDto>> GetUserWishlistsAsync(
        Guid userId,
        int page,
        int pageSize,
        SortingParameter[] sortingParams);
}