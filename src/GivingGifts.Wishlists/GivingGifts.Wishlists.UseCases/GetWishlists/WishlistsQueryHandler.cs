using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishlists;

public class WishlistsQueryHandler : IRequestHandler<WishlistsQuery, Result<PagedData<WishlistDto>>>
{
    private readonly IWishlistsQueryService _wishlistsQueryService;
    private readonly IUserContext _userContext;

    public WishlistsQueryHandler(
        IWishlistsQueryService wishlistsQueryService,
        IUserContext userContext)
    {
        _wishlistsQueryService = wishlistsQueryService;
        _userContext = userContext;
    }

    public async Task<Result<PagedData<WishlistDto>>> Handle(WishlistsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _wishlistsQueryService
            .UserWishlistsAsync(_userContext.UserId, request.Page, request.PageSize);
        return Result<PagedData<WishlistDto>>.Success(result);
    }
}