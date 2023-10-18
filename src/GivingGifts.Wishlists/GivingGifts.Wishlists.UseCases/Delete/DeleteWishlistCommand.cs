using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.Commands.Delete;

public record DeleteWishlistCommand(Guid WishlistId) : IRequest<Result>;