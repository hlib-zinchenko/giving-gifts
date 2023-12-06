namespace GivingGifts.Wishlists.API.DTO.V2;

public class CreateWishlistDto
{
    public string? Name { get; set; }
    public CreateWishDto[] Wishes { get; set; } = [];
}