using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishes;

public class WishesQueryHandler : IRequestHandler<WishesQuery, Result<IEnumerable<WishDto>>>
{
    private readonly IUserContext _userContext;
    private readonly IWishesQueryService _wishesQueryService;

    public WishesQueryHandler(
        IUserContext userContext,
        IWishesQueryService wishesQueryService)
    {
        _userContext = userContext;
        _wishesQueryService = wishesQueryService;
    }

    public async Task<Result<IEnumerable<WishDto>>> Handle(
        WishesQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _wishesQueryService.GetWishesAsync(request.wishlistId);
        var wishes = queryResult.ToArray();
        if (wishes.Any(w => w.UserId != _userContext.UserId))
        {
            return Result<IEnumerable<WishDto>>.NotFound();
        }

        return Result<IEnumerable<WishDto>>.Success(
            wishes.Select(w => new WishDto(w.Id, w.Name, w.Url)));
    }
}