using GivingGifts.SharedKernel.API;
using GivingGifts.Wishlists.API.ApiModels.V2.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Wishlists.API;

public static class DependencyInjection
{
    public static IServiceCollection AddWishlistsApi(this IServiceCollection services)
    {
        services.AddMapping(new WishlistDtoToWishlistProfile());
        services.AddMapping(new WishDtoToWishProfile());
        return services;
    }
}