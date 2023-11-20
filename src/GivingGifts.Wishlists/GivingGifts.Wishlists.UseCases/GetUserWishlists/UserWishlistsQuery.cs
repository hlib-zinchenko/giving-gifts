using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetUserWishlists;

public record UserWishlistsQuery : IRequest<Result<IEnumerable<WishlistDto>>>;