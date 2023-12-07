namespace GivingGifts.Wishlists.API.DTO.V2;

public class WishlistWithWishesDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public WishDto[] Wishes { get; init; } = [];
}