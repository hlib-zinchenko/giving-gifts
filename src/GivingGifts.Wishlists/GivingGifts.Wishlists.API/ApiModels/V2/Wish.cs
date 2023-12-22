// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace GivingGifts.Wishlists.API.ApiModels.V2;

public class Wish
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Url { get; init; }
    public string? Notes { get; set; }
    public string? NameAndNotes { get; set; }
}