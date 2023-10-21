using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.Users.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersUseCases(this IServiceCollection services)
    {
        services.AddMediatR(c => { c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        return services;
    }
}