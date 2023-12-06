using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.V1.CreateWishlist;

public record CreateWishlistCommand(string? Name)
    : IRequest<Result<WishlistDto>>;