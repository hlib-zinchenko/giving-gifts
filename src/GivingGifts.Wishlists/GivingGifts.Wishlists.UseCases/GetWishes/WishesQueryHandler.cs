using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishes;

public class WishesQueryHandler : IRequestHandler<WishesQuery, Result<PagedData<WishDto>>>
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

    public async Task<Result<PagedData<WishDto>>> Handle(
        WishesQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _wishesQueryService
            .GetWishesAsync(_userContext.UserId, request.WishlistId, request.Page, request.PageSize);
        var wishes = queryResult.Data.ToArray();

        return Result<PagedData<WishDto>>.Success(
            queryResult.Map(w => new WishDto(w.Id, w.Name, w.Url, w.Notes)));
    }
}