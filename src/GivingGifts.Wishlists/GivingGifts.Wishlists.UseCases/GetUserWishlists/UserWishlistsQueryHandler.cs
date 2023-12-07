using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetUserWishlists;

public class UserWishlistsQueryHandler : IRequestHandler<UserWishlistsQuery, Result<IEnumerable<WishlistDto>>>
{
    private readonly IUserWishlistsQueryService _userWishlistsQueryService;
    private readonly IUserContext _userContext;

    public UserWishlistsQueryHandler(
        IUserWishlistsQueryService userWishlistsQueryService,
        IUserContext userContext)
    {
        _userWishlistsQueryService = userWishlistsQueryService;
        _userContext = userContext;
    }

    public async Task<Result<IEnumerable<WishlistDto>>> Handle(UserWishlistsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _userWishlistsQueryService
            .UserWishlistsAsync(_userContext.UserId);
        return Result<IEnumerable<WishlistDto>>.Success(result);
    }
}