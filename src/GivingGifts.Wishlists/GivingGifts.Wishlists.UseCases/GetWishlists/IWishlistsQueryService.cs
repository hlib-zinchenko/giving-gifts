using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.DTO;

namespace GivingGifts.Wishlists.UseCases.GetWishlists;

public interface IWishlistsQueryService
{
    Task<PagedData<WishlistDto>> UserWishlistsAsync(
        Guid userId,
        int page,
        int pageSize);
}