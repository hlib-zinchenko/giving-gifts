using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Wishlists.Commands;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}