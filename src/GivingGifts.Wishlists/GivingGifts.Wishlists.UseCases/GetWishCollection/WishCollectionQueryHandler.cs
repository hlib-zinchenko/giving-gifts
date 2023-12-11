using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishCollection;

public class WishCollectionQueryHandler : IRequestHandler<WishCollectionQuery, Result<IEnumerable<WishDto>>>
{
    private readonly IUserContext _userContext;
    private readonly IWishCollectionQueryService _wishesQueryService;

    public WishCollectionQueryHandler(
        IUserContext userContext,
        IWishCollectionQueryService wishesQueryService)
    {
        _userContext = userContext;
        _wishesQueryService = wishesQueryService;
    }

    public async Task<Result<IEnumerable<WishDto>>> Handle(
        WishCollectionQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _wishesQueryService
            .GetWishesAsync(request.WishlistId, request.WishIds);
        var wishes = queryResult.ToArray();
        if (wishes.Any(w => w.UserId != _userContext.UserId))
        {
            return Result<IEnumerable<WishDto>>.NotFound();
        }

        return Result<IEnumerable<WishDto>>.Success(
            wishes.Select(w => new WishDto(w.Id, w.Name, w.Url, w.Notes)));
    }
}