using GivingGifts.Wishlists.Core.Entities;
using GivingGifts.SharedKernel.Core;

namespace GivingGifts.Wishlists.Core;

public interface IWishlistRepository : IRepository<Wishlist>
{
    Task<Wishlist?> GetAsync(Guid id);
    Task<IEnumerable<Wishlist>> GetByUserAsync(Guid userId);
    Task AddAsync(Wishlist wishlist);
    void Delete(Wishlist wishlist);
}