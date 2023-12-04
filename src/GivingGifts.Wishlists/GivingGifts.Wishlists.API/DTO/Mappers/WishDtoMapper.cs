namespace GivingGifts.Wishlists.API.DTO.Mappers;

public static class WishDtoMapper
{
    public static WishDto ToApiDto(GivingGifts.Wishlists.UseCases.WishDto input)
    {
        return new WishDto
        {
            Id = input.Id,
            Name = input.Name,
            Url = input.Url,
        };
    }

    public static WishDto[] ToApiDto(IEnumerable<GivingGifts.Wishlists.UseCases.WishDto> input)
    {
        return input.Select(ToApiDto).ToArray();
    }
}