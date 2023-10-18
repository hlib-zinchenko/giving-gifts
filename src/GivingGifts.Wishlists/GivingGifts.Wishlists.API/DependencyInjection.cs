using GivingGifts.Wishlists.Commands;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Wishlists.API;

public static class DependencyInjection
{
    public static IServiceCollection AddWishlists(this IServiceCollection services)
    {
        return services
            .AddDomain()
            .AddInfrastructure()
            .AddUseCases();
    }
}