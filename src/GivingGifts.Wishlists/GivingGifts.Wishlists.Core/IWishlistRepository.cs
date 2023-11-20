using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.WishlistAggregate;

namespace GivingGifts.Wishlists.Core;

public interface IWishlistRepository : IRepository<Wishlist>
{
    Task<Wishlist?> GetAsync(Guid id);
    Task AddAsync(Wishlist wishlist);
    void Delete(Wishlist wishlist);
}