using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.CreateWishlist;

public record CreateWishlistCommand(string? Name)
    : IRequest<Result<WishlistDto>>;