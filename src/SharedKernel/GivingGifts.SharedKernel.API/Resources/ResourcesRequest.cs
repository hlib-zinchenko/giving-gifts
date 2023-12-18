using GivingGifts.SharedKernel.API.Resources.Utils;
using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.SharedKernel.API.Resources;

public abstract class ResourcesRequest<TResource> :
    IDataShapingRequest<TResource>,
    IPaginatingRequest,
    ISortingRequest<TResource>
{
    private int _page = Paging.DefaultPage;
    private int _pageSize = Paging.DefaultPageSize;
    private readonly Lazy<IEnumerable<SortingRequestEntry>> _sortingParams;
    private readonly Lazy<IEnumerable<string>> _dataShapingFields;

    protected ResourcesRequest()
    {
        _sortingParams = new Lazy<IEnumerable<SortingRequestEntry>>(() => StringsParser.ParseSortByString(OrderBy));
        _dataShapingFields = new Lazy<IEnumerable<string>>(() => StringsParser.ParseDataShapingString(Fields));
    }

    public string? OrderBy { get; set; }
    public string? Fields { get; set; }

    public int Page
    {
        get => _page;
        set => _page = value > 0
            ? value
            : Paging.DefaultPage;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is < 1 or > Paging.MaxPageSize
            ? Paging.DefaultPageSize
            : value;
    }

    public IEnumerable<SortingRequestEntry> GetSortingEntries()
    {
        return _sortingParams.Value;
    }

    public IEnumerable<string> GetDataShapingFields()
    {
        return _dataShapingFields.Value;
    }
}