using GivingGifts.SharedKernel.API.ResultStatusMapping;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.SharedKernel.API;

public static class DependencyInjection
{
    public static IServiceCollection SharedKernelApi(
        this IServiceCollection services, 
        Action<SharedKernelApiConfiguration>? configurationAction = null)
    {
        var configuration = new SharedKernelApiConfiguration(ResultStatusMap.CreateDefault());
        configurationAction?.Invoke(configuration);
        services.AddScoped<ResultStatusMap>(_ => configuration.ResultStatusMap);
        return services;
    }

    public record SharedKernelApiConfiguration(ResultStatusMap ResultStatusMap);
}