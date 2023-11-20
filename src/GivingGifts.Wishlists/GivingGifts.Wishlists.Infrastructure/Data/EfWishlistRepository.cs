using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Core.WishlistAggregate;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data;

public class EfWishlistRepository : IWishlistRepository
{
    private readonly WishlistsDbContextEf _dbContextEf;

    public EfWishlistRepository(WishlistsDbContextEf dbContextEf)
    {
        _dbContextEf = dbContextEf;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContextEf.SaveChangesAsync();
    }

    public Task<Wishlist?> GetAsync(Guid id)
    {
        return _dbContextEf.Wishlists.Include(w => w.Wishes)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task AddAsync(Wishlist wishlist)
    {
        await _dbContextEf.Wishlists.AddAsync(wishlist);
    }

    void IWishlistRepository.Delete(Wishlist wishlist)
    {
        _dbContextEf.Wishlists.Remove(wishlist);
    }
}