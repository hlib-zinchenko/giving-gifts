using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetList;

public record UserWishlistsQuery : IRequest<Result<IEnumerable<WishlistDto>>>;