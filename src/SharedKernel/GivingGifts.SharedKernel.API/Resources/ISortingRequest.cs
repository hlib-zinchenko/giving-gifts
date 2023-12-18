namespace GivingGifts.SharedKernel.API.Resources;

// ReSharper disable once UnusedTypeParameter
public interface ISortingRequest<T> : ISortingRequest;

public interface ISortingRequest
{
    string? OrderBy { get; set; }

    IEnumerable<SortingRequestEntry> GetSortingEntries();
}