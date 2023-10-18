using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Wishlists.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IWishlistRepository, EfWishlistRepository>();
        return services;
    }
}