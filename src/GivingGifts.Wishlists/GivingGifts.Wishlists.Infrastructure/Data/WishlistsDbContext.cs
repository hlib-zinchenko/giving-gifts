using GivingGifts.Wishlists.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data;

public class WishlistsDbContext : DbContext
{
    public DbSet<Wishlist> Wishlists { get; set; } = null!;
    
    public WishlistsDbContext(DbContextOptions<WishlistsDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }
}