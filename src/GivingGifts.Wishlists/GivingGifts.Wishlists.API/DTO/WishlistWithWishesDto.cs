namespace GivingGifts.Wishlists.API.DTO;

public class WishlistWithWishesDto
{
    public Guid UserId { get; init; }
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public WishDto[] Wishes { get; init; } = [];
}