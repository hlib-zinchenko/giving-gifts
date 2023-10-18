using GivingGifts.Wishlists.Core.Entities;
using SharedKernel;

namespace GivingGifts.Wishlists.Core;

public interface IWishlistRepository : IRepository<Wishlist>
{
    Task<Wishlist?> GetAsync(Guid id);
    Task AddAsync(Wishlist wishlist);
    void Delete(Wishlist wishlist);
}