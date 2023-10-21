using System.Reflection;
using GivingGifts.Wishlists.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data;

public class WishlistsDbContext : DbContext
{
    public WishlistsDbContext(DbContextOptions<WishlistsDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    public DbSet<Wishlist> Wishlists { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}