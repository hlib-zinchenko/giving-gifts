using GivingGifts.Wishlists.UseCases;
using GivingGifts.Wishlists.UseCases.GetWishlist;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data.Queries;

public class WishlistQueryService : IWishlistQueryService
{
    private readonly WishlistsDbContextEf _contextEf;

    public WishlistQueryService(WishlistsDbContextEf contextEf)
    {
        _contextEf = contextEf;
    }

    public async Task<WishlistWithWishesDto?> GetWishlist(Guid wishlistId)
    {
        var data = await _contextEf.Wishlists
            .AsNoTracking()
            .Include(c => c.Wishes)
            .FirstOrDefaultAsync(c => c.Id == wishlistId);

        return new WishlistWithWishesDto(
            data.UserId,
            data.Id,
            data.Name,
            data.Wishes.Select(w => new WishDto(w.Id, w.Name, w.Url)).ToArray());
    }
}