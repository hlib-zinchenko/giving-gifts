using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWish;

public class WishQueryHandler : IRequestHandler<WishQuery, Result<WishDto>>
{
    private readonly IWishQueryService _wishQueryService;
    private readonly IUserContext _userContext;

    public WishQueryHandler(
        IWishQueryService wishQueryService,
        IUserContext userContext)
    {
        _wishQueryService = wishQueryService;
        _userContext = userContext;
    }

    public async Task<Result<WishDto>> Handle(WishQuery request, CancellationToken cancellationToken)
    {
        var wish = await _wishQueryService.GetUserWish(request.WishId);

        if (wish == null
            || wish.WishlistId != request.WishlistId
            || wish.UserId != _userContext.UserId)
        {
            return Result<WishDto>.NotFound();
        }

        return Result<WishDto>.Success(new WishDto(wish.Id, wish.Name, wish.Url));
    }
}