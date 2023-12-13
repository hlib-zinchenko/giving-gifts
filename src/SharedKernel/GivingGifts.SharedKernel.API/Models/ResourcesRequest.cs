using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.SharedKernel.API.Models;

public abstract class ResourcesRequest<TResource> :
    IDataShapingRequest<TResource>,
    IPaginateRequest,
    IOrderByRequest
{
    private int _page = Paging.DefaultPage;
    private int _pageSize = Paging.DefaultPageSize;

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


    public string? OrderBy { get; set; }
    public string? Fields { get; set; }
}