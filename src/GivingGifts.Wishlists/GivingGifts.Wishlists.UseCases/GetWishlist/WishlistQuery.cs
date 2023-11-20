using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishlist;

public record WishlistQuery(Guid WishlistId) : IRequest<Result<WishlistWithWishesDto>>;