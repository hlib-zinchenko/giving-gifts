using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetList;

public class UserWishlistsQueryHandler : IRequestHandler<UserWishlistsQuery, Result<IEnumerable<WishlistDto>>>
{
    private readonly IUserContext _userContext;
    private readonly IWishlistRepository _wishlistRepository;

    public UserWishlistsQueryHandler(
        IWishlistRepository wishlistRepository,
        IUserContext userContext)
    {
        _wishlistRepository = wishlistRepository;
        _userContext = userContext;
    }

    public async Task<Result<IEnumerable<WishlistDto>>> Handle(UserWishlistsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        var wishlists = await _wishlistRepository.GetByUserAsync(userId);
        return Result<IEnumerable<WishlistDto>>.Success(
            wishlists.Select(w => new WishlistDto(w.Id, w.Name)));
    }
}