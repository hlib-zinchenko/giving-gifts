using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Core.DTO;
using GivingGifts.Wishlists.Core.WishlistAggregate;
using GivingGifts.Wishlists.Core.WishlistAggregate.Entities;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.CreateWishlist;

public class CreateWishlistCommandHandler : IRequestHandler<CreateWishlistCommand, Result<WishlistDto>>
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

    public async Task<Result<WishlistDto>> Handle(CreateWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlist = new Wishlist(Guid.NewGuid(), _userContext.UserId, request.Name!);
        foreach (var createWishDto in request.Wishes)
        {
            var wish = new Wish(
                Guid.NewGuid(),
                createWishDto.Name!,
                createWishDto.Url,
                createWishDto.Notes);
            wishlist.AddWish(wish);
        }

        await _wishlistRepository.AddAsync(wishlist);
        await _wishlistRepository.SaveChangesAsync();

        return Result<WishlistDto>.Success(new WishlistDto(wishlist.Id, wishlist.Name));
    }
}