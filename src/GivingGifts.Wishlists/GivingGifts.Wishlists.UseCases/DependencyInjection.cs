using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Wishlists.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddWishlistsUseCases(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }
}