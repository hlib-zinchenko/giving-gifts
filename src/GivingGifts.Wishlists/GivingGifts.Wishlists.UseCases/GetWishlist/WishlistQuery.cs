using Ardalis.Result;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishlist;

public record WishlistQuery(Guid WishlistId) : IRequest<Result<WishlistWithWishesDto>>;