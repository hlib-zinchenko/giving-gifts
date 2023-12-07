using Ardalis.Result;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.V1.CreateWishlist;

public record CreateWishlistCommand(string? Name)
    : IRequest<Result<WishlistDto>>;