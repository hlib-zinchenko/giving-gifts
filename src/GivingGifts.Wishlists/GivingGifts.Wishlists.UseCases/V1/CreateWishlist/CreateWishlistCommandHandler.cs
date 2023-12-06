using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Core.WishlistAggregate;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.V1.CreateWishlist;

public class CreateWishlistCommandHandler : IRequestHandler<UseCases.CreateWishlist.CreateWishlistCommand, Result<WishlistDto>>
{
    private readonly IUserContext _userContext;
    private readonly IWishlistRepository _wishlistRepository;

    public CreateWishlistCommandHandler(
        IWishlistRepository wishlistRepository,
        IUserContext userContext)
    {
        _wishlistRepository = wishlistRepository;
        _userContext = userContext;
    }

    public async Task<Result<WishlistDto>> Handle(UseCases.CreateWishlist.CreateWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlist = new Wishlist(Guid.NewGuid(), _userContext.UserId, request.Name!);
        await _wishlistRepository.AddAsync(wishlist);
        await _wishlistRepository.SaveChangesAsync();

        return Result<WishlistDto>.Success(new WishlistDto(wishlist.Id, wishlist.Name));
    }
}