using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.Delete;

public record DeleteWishlistCommand(Guid WishlistId) : IRequest<Result>;