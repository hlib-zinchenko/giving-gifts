using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.UpdateWishlist;

public record UpdateWishlistCommand(Guid WishlistId, string? Name) : IRequest<Result>;