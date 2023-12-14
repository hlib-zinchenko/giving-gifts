using GivingGifts.Wishlists.API.ApiModels.V2.Requests;

namespace GivingGifts.Wishlists.API.ApiModels.V2.Mappers;

public static class WishMapper
{
    public static Wish ToApiModel(Core.DTO.WishDto input)
    {
        return new Wish
        {
            Id = input.Id,
            Name = input.Name,
            Url = input.Url,
            Notes = input.Notes,
        };
    }

    public static Wish[] ToApiModel(IEnumerable<Core.DTO.WishDto> input)
    {
        return input.Select(ToApiModel).ToArray();
    }
    
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