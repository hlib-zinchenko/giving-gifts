using GivingGifts.SharedKernel.Core;

namespace GivingGifts.SharedKernel.API.Resources.Mapping;

public interface IResourceMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
    IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
    SortingParameter[] GetSortingParameters<TSource, TDestination>(ISortingRequest<TDestination> sortingRequest);
    string[] GetConfiguredSortableFields<TSource, TDestination>();
}