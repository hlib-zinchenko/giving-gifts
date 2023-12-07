using GivingGifts.Wishlists.Core.WishlistAggregate;
using GivingGifts.Wishlists.Core.WishlistAggregate.Entities;
using Xunit;

namespace GivingGifts.Wishlists.UnitTests.Core.WishlistAggregate;

public class WishlistAddWishes
{
    private readonly Wishlist _sut = TestData.CreateWishlist();

    [Fact]
    public void AddSuccess()
    {
        var wish1 = CreateWish("75EBDE04-E9A0-4E49-9722-3CEF5327EAFA", "NewWish1");
        var wish2 = CreateWish("86625AE8-3812-47F2-8896-08D1D53B7247", "NewWish2");
        var wishes = new[] { wish1, wish2 };

        _sut.AddWishes(wishes);

        Assert.Equal(2, _sut.Wishes.Count());
        Assert.Contains(wish1, _sut.Wishes);
        Assert.Contains(wish2, _sut.Wishes);
    }

    [Fact]
    public void ThrowExceptionWhenNullNamePassed()
    {
        Assert.Throws<ArgumentNullException>(() => { _sut.AddWishes(null!); });
    }

    private static Wish CreateWish(string id, string name)
    {
        return new Wish(new Guid(id), name, null, null);
    }
}