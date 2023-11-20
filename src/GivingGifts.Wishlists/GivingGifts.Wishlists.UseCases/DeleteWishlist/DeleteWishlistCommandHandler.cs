using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.DeleteWishlist;

public class DeleteWishlistCommandHandler : IRequestHandler<DeleteWishlistCommand, Result>
{
    private readonly IUserContext _userContext;
    private readonly IWishlistRepository _wishlistRepository;

    public DeleteWishlistCommandHandler(
        IWishlistRepository wishlistRepository,
        IUserContext userContext)
    {
        _wishlistRepository = wishlistRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(DeleteWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlist = await _wishlistRepository.GetAsync(request.WishlistId);
        if (wishlist == null || wishlist.UserId != _userContext.UserId) return Result.NotFound();

        _wishlistRepository.Delete(wishlist);
        await _wishlistRepository.SaveChangesAsync();
        return Result.Success();
    }
}