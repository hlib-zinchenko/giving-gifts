using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Wishlists.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddWishlistsUseCases(this IServiceCollection services)
    {
        services.AddMediatR(c => { c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }
}