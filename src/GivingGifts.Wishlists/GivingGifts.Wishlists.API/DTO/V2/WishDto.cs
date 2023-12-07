namespace GivingGifts.Wishlists.API.DTO.V2;

public class WishDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Url { get; init; }
    public string? Notes { get; set; }
}