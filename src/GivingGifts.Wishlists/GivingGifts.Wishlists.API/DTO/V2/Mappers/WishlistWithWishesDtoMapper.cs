namespace GivingGifts.Wishlists.API.DTO.V2.Mappers;

public static class WishlistWithWishesDtoMapper
{
    public static WishlistWithWishesDto ToApiDto(Core.DTO.WishlistWithWishesDto input)
    {
        return new WishlistWithWishesDto
        {
            Id = input.Id,
            Name = input.Name,
            Wishes = WishDtoMapper.ToApiDto(input.Wishes).ToArray(),
        };
    }
}