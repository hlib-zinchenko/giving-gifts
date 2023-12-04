namespace GivingGifts.Wishlists.API.DTO.Mappers;

public static class WishlistWithWishesDtoMapper
{
    public static WishlistWithWishesDto ToApiDto(GivingGifts.Wishlists.UseCases.WishlistWithWishesDto input)
    {
        return new WishlistWithWishesDto
        {
            Id = input.Id,
            Name = input.Name,
            UserId = input.UserId,
            Wishes = WishDtoMapper.ToApiDto(input.Wishes).ToArray(),
        };
    }
}