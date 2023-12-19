using GivingGifts.SharedKernel.API.Resources;

namespace GivingGifts.Wishlists.API.ApiModels.V2;

public class Wishlist
{
    public Guid Id { get; init; }
    [NotDataShapable]
    public string? Name { get; init; }
}
