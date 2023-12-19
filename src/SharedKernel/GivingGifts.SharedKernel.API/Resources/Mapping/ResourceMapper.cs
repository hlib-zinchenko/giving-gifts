using GivingGifts.SharedKernel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace GivingGifts.SharedKernel.API.Resources.Mapping;

public class ResourceMapper : IResourceMapper
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<(Type, Type), object> _cache;

    public ResourceMapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _cache = new Dictionary<(Type, Type), object>();
    }
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        var mapFunction = GetMapper<TSource, TDestination>();
        return mapFunction.Map(source);
    }

    public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
    {
        var mapFunction = GetMapper<TSource, TDestination>();
        return source.Select(mapFunction.Map);
    }

    public SortingParameter[] GetSortingParameters<TSource, TDestination>(
        ISortingRequest<TDestination> sortingRequest)
    {
        var mapFunction = GetMapper<TSource, TDestination>();
        var configuredSortings = mapFunction.GetSortingParameters(sortingRequest);

        return configuredSortings.ToArray();
    }

    public string[] GetConfiguredSortableFields<TSource, TDestination>()
    {
        var mapper = GetMapper<TSource, TDestination>();
        return mapper.GetConfiguredSortableFields();
    }

    private DedicatedResourceMapper<TSource, TDestination> GetMapper<TSource, TDestination>()
    {
        var key = (typeof(TSource), typeof(TDestination));
        if(_cache.TryGetValue(key, out var value))
        {
            return (value as DedicatedResourceMapper<TSource, TDestination>)!;
        }
        var dedicatedMapper = _serviceProvider.GetRequiredService<DedicatedResourceMapper<TSource, TDestination>>();
        _cache[key] = dedicatedMapper;
        return dedicatedMapper;
    }
}