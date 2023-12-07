using GivingGifts.Wishlists.Core.WishlistAggregate;
using Xunit;

namespace GivingGifts.Wishlists.UnitTests.Core.WishlistAggregate;

public class WishlistUpdate
{
    private readonly Wishlist _sut = TestData.CreateWishlist();

    [Fact]
    public void UpdateSuccess()
    {
        const string newName = "NewTestName";
        _sut.Update(newName);

        Assert.Equal(newName, _sut.Name);
    }

    [Fact]
    public void ThrowExceptionWhenNullNamePassed()
    {
        Assert.Throws<ArgumentNullException>(() => { _sut.Update(null!); });
    }
}