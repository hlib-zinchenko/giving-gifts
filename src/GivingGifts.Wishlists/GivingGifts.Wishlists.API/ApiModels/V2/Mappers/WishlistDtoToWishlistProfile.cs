using GivingGifts.SharedKernel.API.Resources.Mapping;
using GivingGifts.Wishlists.Core.DTO;

namespace GivingGifts.Wishlists.API.ApiModels.V2.Mappers;

public class WishlistDtoToWishlistProfile : IResourceMappingProfile<WishlistDto, Wishlist>
{
    public ResourceMappingConfiguration<WishlistDto, Wishlist> CreateMappingConfiguration()
    {
        return new ResourceMappingConfiguration<WishlistDto, Wishlist>()
            .CreateMap(s => s.Name, d => d.Name, true)
            .CreateMap(s => s.Id, d => d.Id,true);
    }
}