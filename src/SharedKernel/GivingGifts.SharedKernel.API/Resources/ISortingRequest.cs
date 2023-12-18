namespace GivingGifts.SharedKernel.API.Resources;

public interface ISortingRequest<T> : ISortingRequest;

public interface ISortingRequest
{
    string? OrderBy { get; set; }

    IEnumerable<SortingRequestEntry> GetSortingEntries();
}