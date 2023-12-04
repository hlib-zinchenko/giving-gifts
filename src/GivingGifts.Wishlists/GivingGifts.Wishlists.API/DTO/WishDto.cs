namespace GivingGifts.Wishlists.API.DTO;

public class WishDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Url { get; init; }
}