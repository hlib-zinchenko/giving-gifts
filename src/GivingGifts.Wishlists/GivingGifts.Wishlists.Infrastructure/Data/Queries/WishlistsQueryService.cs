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

    public Task<PagedData<WishlistDto>> GetUserWishlistsAsync(
        Guid userId, int page, int pageSize, SortingParameter[] sortingParams)
    {
        return _wishlistsDbContextEf
            .Wishlists
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .Select(w => new WishlistDto {  Id = w.Id, Name = w.Name })
            .OrderBy(sortingParams)
            .ToPagedDataAsync(page, pageSize);
    }
}