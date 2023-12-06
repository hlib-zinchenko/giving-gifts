namespace GivingGifts.Wishlists.API.DTO.V2.Mappers;

public static class WishlistDtoMapper
{
    public static WishlistDto ToApiDto(GivingGifts.Wishlists.UseCases.WishlistDto input)
    {
        return new WishlistDto
        {
            Id = input.Id,
            Name = input.Name,
        };
    }

    public static WishlistDto[] ToApiDto(IEnumerable<GivingGifts.Wishlists.UseCases.WishlistDto> input)
    {
        return input.Select(ToApiDto).ToArray();
    }
}