using GivingGifts.SharedKernel.Core;

namespace GivingGifts.SharedKernel.API.Resources.Mapping;

public interface ISortingParametersProvider<TDestination>
{
    IEnumerable<SortingParameter> GetSortingParameters(ISortingRequest<TDestination> sortingRequest);
    bool ValidateRequestEntries(
        ISortingRequest<TDestination> sortingRequest,
        out SortingRequestEntry[] invalidEntries);
}