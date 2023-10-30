using GivingGifts.Wishlists.Core.WishlistAggregate;

namespace GivingGifts.Wishlists.UnitTests.Core.WishlistAggregate;

public static class TestData
{
    public static readonly Guid InitialId =new ("8EE25A6E-FF92-4F28-A1B1-86F371A4A12A");
    public static readonly Guid InitialUserId = new("3F5F3BA1-C843-4804-95A7-CF680DDB122F");
    public const string TestInitialName = "TestInitialName";

    public static Wishlist CreateWishlist() => new(InitialId, InitialUserId, TestInitialName);
}