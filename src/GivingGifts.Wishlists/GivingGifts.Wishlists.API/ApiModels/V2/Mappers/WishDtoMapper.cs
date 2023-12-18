using GivingGifts.Wishlists.API.ApiModels.V2.Requests;

namespace GivingGifts.Wishlists.API.ApiModels.V2.Mappers;

public static class WishDtoMapper
{
    public static UpdateWishRequest ToUpdateWishRequest(Core.DTO.WishDto input)
    {
        return new UpdateWishRequest
        {
            Name = input.Name,
            Url = input.Url,
            Notes = input.Notes,
        };
    }
}