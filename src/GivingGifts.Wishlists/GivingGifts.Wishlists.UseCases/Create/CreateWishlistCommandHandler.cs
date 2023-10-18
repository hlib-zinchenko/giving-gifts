using Ardalis.Result;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Core.Entities;
using MediatR;
using SharedKernel;

namespace GivingGifts.Wishlists.Commands.Create;

public class CreateWishlistCommandHandler : IRequestHandler<CreateWishlistCommand, Result<CreateWishlistCommandResult>>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUserContext _userContext;

    public CreateWishlistCommandHandler(
        IWishlistRepository wishlistRepository,
        IUserContext userContext)
    {
        _wishlistRepository = wishlistRepository;
        _userContext = userContext;
    }
    public async Task<Result<CreateWishlistCommandResult>> Handle(CreateWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlist = new Wishlist(_userContext.UserId, request.Name!);
        await _wishlistRepository.AddAsync(wishlist);
        await _wishlistRepository.SaveChangesAsync();

        return Result<CreateWishlistCommandResult>.Success(new(wishlist.Id, wishlist.Name));
    }
}