namespace GivingGifts.Wishlists.API.ApiModels.V2.Mappers;

public static class WishlistMapper
{
    public static Wishlist ToApiModel(Core.DTO.WishlistDto input)
    {
        return new Wishlist
        {
            Id = input.Id,
            Name = input.Name,
        };
    }

    public static Wishlist[] ToApiModel(IEnumerable<Core.DTO.WishlistDto> input)
    {
        return input.Select(ToApiModel).ToArray();
    }
}