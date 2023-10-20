using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.Create;

public record CreateWishlistCommand(string? Name)
    : IRequest<Result<WishlistDto>>;