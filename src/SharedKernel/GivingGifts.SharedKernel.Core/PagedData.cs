namespace GivingGifts.SharedKernel.Core;

public class PagedData<T>
{
    public int TotalCount { get; }
    public IEnumerable<T> Data { get; }
    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasNext => CurrentPage < TotalPages;
    public bool HasPrevious => CurrentPage > 1 && CurrentPage <= TotalPages + 1;

    public PagedData(
        IEnumerable<T> data,
        int currentPage,
        int pageSize,
        int totalCount)
    {
        Data = data;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public PagedData<TDestination> Map<TDestination>(Func<T, TDestination> mapAction)
    {
        return new PagedData<TDestination>(
            Data.Select(mapAction),
            CurrentPage,
            PageSize,
            TotalCount);
    }
}