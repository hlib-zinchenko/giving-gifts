using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Core.WishlistAggregate.Entities;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.CreateWish;

public class CreateWishCommandHandler : IRequestHandler<CreateWishCommand, Result<WishDto>>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUserContext _userContext;

    public CreateWishCommandHandler(
        IWishlistRepository wishlistRepository,
        IUserContext userContext)
    {
        _wishlistRepository = wishlistRepository;
        _userContext = userContext;
    }

    public async Task<Result<WishDto>> Handle(CreateWishCommand request, CancellationToken cancellationToken)
    {
        var wishlist = await _wishlistRepository.GetAsync(request.WishlistId);

        if (wishlist == null || wishlist.UserId != _userContext.UserId)
        {
            return Result<WishDto>.NotFound();
        }

        var wish = new Wish(Guid.NewGuid(), request.Name!, request.Url, request.Notes);
        wishlist.AddWish(wish);

        await _wishlistRepository.SaveChangesAsync();

        return Result<WishDto>.Success(new WishDto(wish.Id, wish.Name, wish.Url));
    }
}