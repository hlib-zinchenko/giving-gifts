namespace GivingGifts.Wishlists.API.DTO.V2.Mappers;

public static class WishDtoMapper
{
    public static WishDto ToApiDto(Core.DTO.WishDto input)
    {
        return new WishDto
        {
            Id = input.Id,
            Name = input.Name,
            Url = input.Url,
            Notes = input.Notes,
        };
    }

    public static WishDto[] ToApiDto(IEnumerable<Core.DTO.WishDto> input)
    {
        return input.Select(ToApiDto).ToArray();
    }
    
    public static UpdateWishDto ToUpdateWishDto(Core.DTO.WishDto input)
    {
        return new UpdateWishDto
        {
            Name = input.Name,
            Url = input.Url,
            Notes = input.Notes,
        };
    }
}