using GivingGifts.SharedKernel.Core;
using GivingGifts.SharedKernel.Infrastructure;
using GivingGifts.Wishlists.Core.DTO;
using GivingGifts.Wishlists.UseCases.GetWishes;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data.Queries;

public class WishesQueryService : IWishesQueryService
{
    private readonly WishlistsDbContextEf _wishlistsDbContextEf;

    public WishesQueryService(WishlistsDbContextEf wishlistsDbContextEf)
    {
        _wishlistsDbContextEf = wishlistsDbContextEf;
    }

    public Task<PagedData<WishDto>> GetWishesAsync(
        Guid userId,
        Guid wishlistId,
        int page,
        int pageSize,
        SortingParameter[] sortingParams)
    {
        return _wishlistsDbContextEf
            .Wishlists
            .AsNoTracking()
            .Where(w => w.Id == wishlistId && w.UserId == userId)
            .SelectMany(w => w.Wishes)
            .Select(w => new WishDto
            {
                Id = w.Id,
                Name = w.Name,
                Notes = w.Notes,
                Url = w.Url,
            })
            .OrderBy(sortingParams)
            .ToPagedDataAsync(page, pageSize);
    }
}