using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.DeleteWishlist;

public record DeleteWishlistCommand(Guid WishlistId) : IRequest<Result>;