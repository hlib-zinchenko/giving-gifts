using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.Update;

public class UpdateWishlistCommandHandler : IRequestHandler<UpdateWishlistCommand, Result>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUserContext _userContext;

    public UpdateWishlistCommandHandler(
        IWishlistRepository wishlistRepository,
        IUserContext userContext)
    {
        _wishlistRepository = wishlistRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UpdateWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlist = await _wishlistRepository.GetAsync(request.WishlistId);
        if (wishlist == null || wishlist.UserId != _userContext.UserId) return Result.NotFound();

        wishlist.Update(request.Name!);
        await _wishlistRepository.SaveChangesAsync();

        return Result.Success();
    }
}