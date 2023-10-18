using Ardalis.Result;
using GivingGifts.Wishlists.Core;
using MediatR;
using SharedKernel;

namespace GivingGifts.Wishlists.Commands.Delete;

public class DeleteWishlistCommandHandler : IRequestHandler<DeleteWishlistCommand, Result>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUserContext _userContext;

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
        if (wishlist == null || wishlist.UserId != _userContext.UserId)
        {
            return Result.NotFound();
        }
        
        _wishlistRepository.Delete(wishlist);
        await _wishlistRepository.SaveChangesAsync();
        return Result.Success();
    }
}