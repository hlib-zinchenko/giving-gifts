namespace GivingGifts.Wishlists.API.ApiModels.V2.Requests;

public class CreateWishlistRequest
{
    public string? Name { get; set; }
    public CreateWishRequest[] Wishes { get; set; } = [];
}