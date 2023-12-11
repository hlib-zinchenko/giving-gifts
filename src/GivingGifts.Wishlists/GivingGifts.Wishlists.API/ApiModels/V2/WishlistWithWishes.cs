namespace GivingGifts.Wishlists.API.ApiModels.V2;

public class WishlistWithWishes
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public Wish[] Wishes { get; init; } = [];
}