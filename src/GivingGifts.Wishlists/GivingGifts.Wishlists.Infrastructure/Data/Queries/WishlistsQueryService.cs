using GivingGifts.SharedKernel.Core;
using GivingGifts.SharedKernel.Infrastructure;
using GivingGifts.Wishlists.Core.DTO;
using GivingGifts.Wishlists.UseCases.GetWishlists;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data.Queries;

public class WishlistsQueryService : IWishlistsQueryService
{
    private readonly WishlistsDbContextEf _wishlistsDbContextEf;

    public WishlistsQueryService(
        WishlistsDbContextEf wishlistsDbContextEf)
    {
        _wishlistsDbContextEf = wishlistsDbContextEf;
    }

    public Task<PagedData<WishlistDto>> UserWishlistsAsync(Guid userId, int page, int pageSize)
    {
        return _wishlistsDbContextEf
            .Wishlists
            .AsNoTracking()
            .Select(w => new WishlistDto(w.Id, w.Name))
            .ToPagedDataAsync(page, pageSize);
    }
}