using GivingGifts.SharedKernel.API.Resources.Mapping;
using GivingGifts.SharedKernel.API.Resources.RequestValidation;
using GivingGifts.SharedKernel.API.Resources.RequestValidation.Validators;
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

        return services.AddScoped<ResultStatusMap>(_ => configuration.ResultStatusMap)
            .AddScoped<IResourceMapper, ResourceMapper>()
            .AddRequestValidation();
    }

    public static IServiceCollection AddMapping<TSource, TDestination>(
        this IServiceCollection services,
        IResourceMappingProfile<TSource, TDestination> profile)
    {
        var mapper = profile.CreateMappingConfiguration().BuildMapper();
        services.AddSingleton(mapper);

        return services;
    }

    private static IServiceCollection AddRequestValidation(this IServiceCollection services)
    {
        services.AddScoped<IResourcesRequestValidator, ResourcesRequestValidator>();
        services.AddScoped<IResourceRequestValidator, ResourceRequestValidator>();
        services.AddScoped<IDataShapingRequestValidator, DataShapingRequestValidator>();
        services.AddScoped<ISortingRequestValidator, SortingRequestValidator>();

        return services;
    }

    public record SharedKernelApiConfiguration(ResultStatusMap ResultStatusMap);
}