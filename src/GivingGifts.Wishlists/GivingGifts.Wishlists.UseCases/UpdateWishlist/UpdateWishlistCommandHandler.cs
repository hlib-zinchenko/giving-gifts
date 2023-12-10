using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.UpdateWishlist;

public class UpdateWishlistCommandHandler : IRequestHandler<UpdateWishlistCommand, Result>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUserContext _userContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateWishlistCommandHandler(
        IWishlistRepository wishlistRepository,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
    {
        _wishlistRepository = wishlistRepository;
        _userContext = userContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(UpdateWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlist = await _wishlistRepository.GetAsync(request.WishlistId);
        if (wishlist == null || wishlist.UserId != _userContext.UserId) return Result.NotFound();

        wishlist.Update(request.Name!, _dateTimeProvider);

        await _wishlistRepository.SaveChangesAsync();

        return Result.Success();
    }
}