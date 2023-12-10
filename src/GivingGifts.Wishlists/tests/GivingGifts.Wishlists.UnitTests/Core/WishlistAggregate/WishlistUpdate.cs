using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.WishlistAggregate;
using NSubstitute;
using Xunit;

namespace GivingGifts.Wishlists.UnitTests.Core.WishlistAggregate;

public class WishlistUpdate
{
    private readonly Wishlist _sut = TestData.CreateWishlist();
    private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();

    public WishlistUpdate()
    {
        _dateTimeProvider.UtcNow.Returns(DateTime.UtcNow);
    }
    [Fact]
    public void UpdateSuccess()
    {
        const string newName = "NewTestName";
        _sut.Update(newName, _dateTimeProvider);

        Assert.Equal(newName, _sut.Name);
    }

    [Fact]
    public void ThrowExceptionWhenNullNamePassed()
    {
        Assert.Throws<ArgumentNullException>(() => { _sut.Update(null!, _dateTimeProvider); });
    }
}