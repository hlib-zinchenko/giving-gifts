using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.UpdateWish;

public record UpdateWishCommand(Guid WishlistId, Guid WishId, string? Name, string? Url, string? Notes) : IRequest<Result>;