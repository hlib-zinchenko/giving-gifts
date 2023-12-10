using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.UpdateWish;

public class UpdateWishCommandHandler : IRequestHandler<UpdateWishCommand, Result>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUserContext _userContext;

    public UpdateWishCommandHandler(
        IWishlistRepository wishlistRepository,
        IUserContext userContext)
    {
        _wishlistRepository = wishlistRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(UpdateWishCommand request, CancellationToken cancellationToken)
    {
        var wishlist = await _wishlistRepository.GetAsync(request.WishlistId);
        if (wishlist == null || wishlist.UserId != _userContext.UserId)
        {
            return Result.NotFound();
        }

        var wish = wishlist.Wishes.FirstOrDefault(w => w.Id == request.WishId);

        if (wish == null)
        {
            return Result.NotFound();
        }

        wish.Update(request.Name!, request.Url, request.Notes);

        await _wishlistRepository.SaveChangesAsync();

        return Result.Success();
    }
}