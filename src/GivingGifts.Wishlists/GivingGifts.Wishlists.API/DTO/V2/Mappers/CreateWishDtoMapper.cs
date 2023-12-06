namespace GivingGifts.Wishlists.API.DTO.V2.Mappers;

public static class CreateWishDtoMapper
{
    public static Wishlists.UseCases.CreateWishDto ToCommandDto(CreateWishDto dto)
    {
        return new UseCases.CreateWishDto(dto.Name, dto.Url, dto.Notes);
    }
    
    public static Wishlists.UseCases.CreateWishDto[] ToCommandDto(IEnumerable<CreateWishDto> dto)
    {
        return dto.Select(ToCommandDto).ToArray();
    }
}