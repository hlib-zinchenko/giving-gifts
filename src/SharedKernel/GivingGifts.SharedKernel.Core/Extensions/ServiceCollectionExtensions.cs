using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.SharedKernel.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidatableOptions<TOptions>(
        this IServiceCollection serviceCollection,
        string configurationSection)
        where TOptions : class
    {
        serviceCollection.AddOptions<TOptions>()
            .BindConfiguration(configurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return serviceCollection;
    }
}