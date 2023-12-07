namespace GivingGifts.Wishlists.UseCases.GetWish
{
    public class UserWishDto
    {
        public Guid UserId { get; init; }
        public Guid WishlistId { get; init; }
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Url { get; init; }
        public string? Notes { get; init; }
    }
}