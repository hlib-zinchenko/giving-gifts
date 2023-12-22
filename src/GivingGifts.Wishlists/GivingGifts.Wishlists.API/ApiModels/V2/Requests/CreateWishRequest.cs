// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace GivingGifts.Wishlists.API.ApiModels.V2.Requests;

public class CreateWishRequest
{
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? Notes { get; set; }
}