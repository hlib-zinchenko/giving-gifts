using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.Update;

public record UpdateWishlistCommand(Guid WishlistId, string? Name) : IRequest<Result>;