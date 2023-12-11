namespace GivingGifts.Wishlists.API.ApiModels.V2.Mappers;

public static class WishlistWithWishesMapper
{
    public static WishlistWithWishes ToApiModel(Core.DTO.WishlistWithWishesDto input)
    {
        return new WishlistWithWishes
        {
            Id = input.Id,
            Name = input.Name,
            Wishes = WishMapper.ToApiModel(input.Wishes).ToArray(),
        };
    }
}