namespace GivingGifts.Wishlists.API.ApiModels.V1.Mappers;

public static class WishlistDtoMapper
{
    public static WishlistDto ToApiDto(Core.DTO.WishlistDto input)
    {
        return new WishlistDto
        {
            Id = input.Id,
            Name = input.Name,
        };
    }

    public static WishlistDto[] ToApiDto(IEnumerable<Core.DTO.WishlistDto> input)
    {
        return input.Select(ToApiDto).ToArray();
    }
}