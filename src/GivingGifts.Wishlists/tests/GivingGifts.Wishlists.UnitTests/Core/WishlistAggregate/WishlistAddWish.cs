using GivingGifts.Wishlists.Core.WishlistAggregate;
using GivingGifts.Wishlists.Core.WishlistAggregate.Entities;
using Xunit;

namespace GivingGifts.Wishlists.UnitTests.Core.WishlistAggregate;

public class WishlistAddWish
{
    private readonly Wishlist _sut = TestData.CreateWishlist();

    [Fact]
    public void AddSuccess()
    {
        var wish = new Wish(
            new Guid("75EBDE04-E9A0-4E49-9722-3CEF5327EAFA"),
            "NewWish",
            null,
            null);

        _sut.AddWish(wish);

        Assert.Equal(wish, _sut.Wishes.First());
        Assert.Single(_sut.Wishes);
    }
}