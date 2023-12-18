using GivingGifts.SharedKernel.API.Resources.Mapping;
using GivingGifts.Wishlists.Core.DTO;

namespace GivingGifts.Wishlists.API.ApiModels.V2.Mappers;

public class WishDtoToWishProfile : IResourceMappingProfile<WishDto, Wish>
{
    public ResourceMappingConfiguration<WishDto, Wish> CreateMappingConfiguration()
    {
        return new ResourceMappingConfiguration<WishDto, Wish>()
            .CreateMap(s => s.Name, d => d.Name, true)
            .CreateMap(s => s.Id, d => d.Id, true)
            .CreateMap(s => s.Notes, d => d.Notes, true)
            .CreateMap(s => s.Url, s => s.Url, true)
            .CreateMap(
                s => s.Name + " " + s.Notes,
                d => d.NameAndNotes,
                c => c
                    .Add(s => s.Name)
                    .Add(s => s.Notes, true));
    }
}