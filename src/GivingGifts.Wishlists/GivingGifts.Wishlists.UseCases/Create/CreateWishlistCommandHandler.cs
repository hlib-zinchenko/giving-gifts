using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Core.Entities;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.Create;

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
        var wishlist = new Wishlist(_userContext.UserId, request.Name!);
        await _wishlistRepository.AddAsync(wishlist);
        await _wishlistRepository.SaveChangesAsync();

        return Result<WishlistDto>.Success(new WishlistDto(wishlist.Id, wishlist.Name));
    }
}