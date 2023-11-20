using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.DeleteWish;

public record DeleteWishCommand(Guid WishlistId, Guid WishId) : IRequest<Result>;