namespace GivingGifts.Wishlists.API.ApiModels.V2.Mappers;

public static class CreateWishRequestMapper
{
    public static Core.DTO.CreateWishDto ToCommandDto(CreateWishRequest request)
    {
        return new Core.DTO.CreateWishDto(request.Name, request.Url, request.Notes);
    }
    
    public static Core.DTO.CreateWishDto[] ToCommandDto(IEnumerable<CreateWishRequest> dto)
    {
        return dto.Select(ToCommandDto).ToArray();
    }
}