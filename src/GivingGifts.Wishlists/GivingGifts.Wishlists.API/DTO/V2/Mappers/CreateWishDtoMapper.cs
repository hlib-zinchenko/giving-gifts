namespace GivingGifts.Wishlists.API.DTO.V2.Mappers;

public static class CreateWishDtoMapper
{
    public static Core.DTO.CreateWishDto ToCommandDto(CreateWishDto dto)
    {
        return new Core.DTO.CreateWishDto(dto.Name, dto.Url, dto.Notes);
    }
    
    public static Core.DTO.CreateWishDto[] ToCommandDto(IEnumerable<CreateWishDto> dto)
    {
        return dto.Select(ToCommandDto).ToArray();
    }
}