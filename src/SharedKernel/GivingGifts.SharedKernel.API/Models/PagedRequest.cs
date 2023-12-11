using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.SharedKernel.API.Models;

public abstract class PagedRequest
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
}