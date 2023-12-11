using GivingGifts.SharedKernel.Core;
using GivingGifts.SharedKernel.Infrastructure;
using GivingGifts.Wishlists.UseCases.GetWishes;
using Microsoft.EntityFrameworkCore;
using UserWishDto = GivingGifts.Wishlists.UseCases.GetWishes.UserWishDto;

namespace GivingGifts.Wishlists.Infrastructure.Data.Queries;

public class WishesQueryService : IWishesQueryService
{
    private readonly WishlistsDbContextEf _wishlistsDbContextEf;

    public WishesQueryService(WishlistsDbContextEf wishlistsDbContextEf)
    {
        _wishlistsDbContextEf = wishlistsDbContextEf;
    }

    public Task<PagedData<UserWishDto>> GetWishesAsync(Guid userId, Guid wishlistId, int page, int pageSize)
    {
        return _wishlistsDbContextEf
            .Wishlists
            .AsNoTracking()
            .Where(w => w.Id == wishlistId && w.UserId == userId)
            .SelectMany(w => w.Wishes)
            .Select(w => new UserWishDto
            {
                Id = w.Id,
                Name = w.Name,
                Notes = w.Notes,
                UserId = userId,
                Url = w.Url,
                WishlistId = wishlistId
            })
            .ToPagedDataAsync(page, pageSize);
    }
}