using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Wishlists.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddWishlistsDomain(this IServiceCollection services)
    {
        return services;
    }
}