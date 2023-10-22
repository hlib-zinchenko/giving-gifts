using System.Reflection;
using GivingGifts.SharedKernel.Core.Behaviours;
using MediatR;
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

    public static IServiceCollection AddMediatR(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        serviceCollection.AddMediatR(c => c.RegisterServicesFromAssemblies(assemblies));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(LoggingBehaviour<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>));
        return serviceCollection;
    }
}