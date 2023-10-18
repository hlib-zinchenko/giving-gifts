using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Implementations;

namespace SharedKernel;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedKernel(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }
}