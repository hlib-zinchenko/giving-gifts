using GivingGifts.Wishlists.Core.WishlistAggregate;
using GivingGifts.Wishlists.Core.WishlistAggregate.Entities;
using Xunit;

namespace GivingGifts.Wishlists.UnitTests.Core.WishlistAggregate;

public class WishlistRemoveWish
{
    private readonly Wish _wish1 = new(new Guid("71C37617-C3B4-4675-A24A-CCDC8B2F5AA6"),"FirstWishName", null, null);
    private readonly Wish _wish2 = new(new Guid("8DECFC39-E3B2-4791-A6D0-51848AA4C2BB"), "SecondWishName", null, null);
    private readonly Wish _wish3 = new(new Guid("1A1A6507-3E82-417B-8FDB-C98E72135FDD"), "ThirdWishName", null, null);

    private readonly Wishlist _sut;

    public WishlistRemoveWish()
    {
        _sut = TestData.CreateWishlist();
        _sut.AddWish(_wish1);
        _sut.AddWish(_wish2);
        _sut.AddWish(_wish3);
    }

    [Fact]
    public void RemoveSuccess()
    {
        var initialNumberOfWishes = _sut.Wishes.Count();
        var removedWish = _wish2;
        _sut.RemoveWish(removedWish);
        Assert.Equal(initialNumberOfWishes - 1, _sut.Wishes.Count());
        Assert.DoesNotContain(_sut.Wishes, w => w.Equals(removedWish));
        Assert.DoesNotContain(_sut.Wishes, w => w.Id == removedWish.Id);
    }
}