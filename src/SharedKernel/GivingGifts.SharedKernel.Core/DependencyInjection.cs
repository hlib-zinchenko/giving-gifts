using GivingGifts.SharedKernel.Core.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.SharedKernel.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedKernelCore(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }
}