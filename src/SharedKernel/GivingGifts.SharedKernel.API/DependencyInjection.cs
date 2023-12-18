using GivingGifts.SharedKernel.API.Resources.Mapping;
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
        services.AddScoped<IResourceMapper, ResourceMapper>();
        return services;
    }

    public static IServiceCollection AddMapping<TSource, TDestination>(
        this IServiceCollection services, 
        IResourceMappingProfile<TSource, TDestination> profile)
    {
        var mapper = profile.CreateMappingConfiguration().BuildMapper();
        services.AddSingleton(mapper);

        return services;
    }

    public record SharedKernelApiConfiguration(ResultStatusMap ResultStatusMap);
}