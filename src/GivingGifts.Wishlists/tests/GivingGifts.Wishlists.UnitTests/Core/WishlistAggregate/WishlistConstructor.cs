using GivingGifts.Wishlists.Core.WishlistAggregate;
using Xunit;

namespace GivingGifts.Wishlists.UnitTests.Core.WishlistAggregate;

public class WishlistConstructor
{
    [Fact]
    public void InitializesProperties()
    {
        var wishlist = new Wishlist(TestData.InitialId, TestData.InitialUserId, TestData.TestInitialName);

        Assert.Equal(TestData.InitialId, wishlist.Id);
        Assert.Equal(TestData.InitialUserId, wishlist.UserId);
        Assert.Equal(TestData.TestInitialName, wishlist.Name);
    }
}