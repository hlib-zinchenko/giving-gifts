using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishlist;

public class WishlistQueryHandler : IRequestHandler<WishlistQuery, Result<WishlistWithWishesDto>>
{
    private readonly IWishlistQueryService _queryService;
    private readonly IUserContext _userContext;

    public WishlistQueryHandler(
        IWishlistQueryService queryService,
        IUserContext userContext)
    {
        _queryService = queryService;
        _userContext = userContext;
    }

    public async Task<Result<WishlistWithWishesDto>> Handle(WishlistQuery request, CancellationToken cancellationToken)
    {
        var wishlist = await _queryService.GetWishlist(request.WishlistId);
        if (wishlist == null || wishlist.UserId != _userContext.UserId)
        {
            return Result<WishlistWithWishesDto>.NotFound();
        }

        return Result<WishlistWithWishesDto>.Success(wishlist);
    }
}