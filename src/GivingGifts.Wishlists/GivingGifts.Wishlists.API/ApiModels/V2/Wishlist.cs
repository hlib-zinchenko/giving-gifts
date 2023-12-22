using GivingGifts.SharedKernel.API.Resources;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GivingGifts.Wishlists.API.ApiModels.V2;

// ReSharper disable once ClassNeverInstantiated.Global
public class Wishlist
{
    public Guid Id { get; init; }
    [NotDataShapable]
    public string? Name { get; init; }
}
