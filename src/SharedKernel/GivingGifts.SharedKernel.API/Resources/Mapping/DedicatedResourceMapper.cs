using GivingGifts.SharedKernel.Core;

namespace GivingGifts.SharedKernel.API.Resources.Mapping;

public class DedicatedResourceMapper<TSource, TDestination> : ISortingParametersProvider<TDestination>
{
    private readonly Func<TSource, TDestination> _mapFunction;
    private readonly IReadOnlyDictionary<string, List<SortablePropertyInfo>> _sortingConfigurations;

    public DedicatedResourceMapper(
        Func<TSource, TDestination> mapFunction,
        IReadOnlyDictionary<string, List<SortablePropertyInfo>> sortings)
    {
        _mapFunction = mapFunction;
        _sortingConfigurations = sortings;
    }

    public TDestination Map(TSource source)
    {
        return _mapFunction(source);
    }

    public IEnumerable<SortingParameter> GetSortingParameters(ISortingRequest<TDestination> sortingRequest)
    {
        var result = new List<SortingParameter>();
        foreach (var sorting in sortingRequest.GetSortingEntries())
        {
            if (!_sortingConfigurations.TryGetValue(
                    sorting.SortBy, out var propertyInfos))
            {
                throw new InvalidOperationException(
                    $"Missing sorting configuration for {sorting.SortBy} in {typeof(TSource)}");
            }

            result.AddRange(propertyInfos.Select(c => c.ToSortingParams(sorting.SortDirection)));
        }

        return result;
    }

    public string[] GetConfiguredSortableFields()
    {
        return _sortingConfigurations.Keys.ToArray();
    }
}