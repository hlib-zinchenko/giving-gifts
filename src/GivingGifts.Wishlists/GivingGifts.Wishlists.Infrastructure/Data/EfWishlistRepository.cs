using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data;

public class EfWishlistRepository : IWishlistRepository
{
    private readonly WishlistsDbContext _dbContext;

    public EfWishlistRepository(WishlistsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public Task<Wishlist?> GetAsync(Guid id)
    {
        return _dbContext.Wishlists.Include(w => w.Wishes)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<IEnumerable<Wishlist>> GetByUserAsync(Guid userId)
    {
        return await _dbContext.Wishlists.Where(w => w.UserId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(Wishlist wishlist)
    {
        await _dbContext.Wishlists.AddAsync(wishlist);
    }

    void IWishlistRepository.Delete(Wishlist wishlist)
    {
        _dbContext.Wishlists.Remove(wishlist);
    }
}