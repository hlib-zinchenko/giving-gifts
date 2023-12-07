using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core;
using GivingGifts.Wishlists.Core.DTO;
using GivingGifts.Wishlists.Core.WishlistAggregate.Entities;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.CreateWishes;

public class CreateWishesCommandHandler : IRequestHandler<CreateWishesCommand, Result<WishDto[]>>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IUserContext _userContext;

    public CreateWishesCommandHandler(
        IWishlistRepository wishlistRepository,
        IUserContext userContext)
    {
        _wishlistRepository = wishlistRepository;
        _userContext = userContext;
    }

    public async Task<Result<WishDto[]>> Handle(CreateWishesCommand request, CancellationToken cancellationToken)
    {
        var wishlist = await _wishlistRepository.GetAsync(request.WishlistId);
        if (wishlist == null || wishlist.UserId != _userContext.UserId)
        {
            return Result<WishDto[]>.NotFound();
        }

        var wishes = request.Wishes.Select(w => new Wish(
            Guid.NewGuid(),
            w.Name!,
            w.Url,
            w.Notes)).ToArray();

        wishlist.AddWishes(wishes);

        Result<WishDto[]>.Invalid();
        await _wishlistRepository.SaveChangesAsync();
        return Result<WishDto[]>.Success(
            wishes.Select(w => new WishDto(w.Id, w.Name, w.Url, w.Notes)).ToArray());
    }
}