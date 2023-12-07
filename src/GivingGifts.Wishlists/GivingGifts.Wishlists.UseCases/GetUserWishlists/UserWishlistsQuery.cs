using Ardalis.Result;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetUserWishlists;

public record UserWishlistsQuery : IRequest<Result<IEnumerable<WishlistDto>>>;