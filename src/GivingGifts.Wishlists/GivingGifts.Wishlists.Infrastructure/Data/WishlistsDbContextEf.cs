using System.Reflection;
using GivingGifts.Wishlists.Core.WishlistAggregate;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data;

public class WishlistsDbContextEf : DbContext
{
    public WishlistsDbContextEf(DbContextOptions<WishlistsDbContextEf> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    public DbSet<Wishlist> Wishlists { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}