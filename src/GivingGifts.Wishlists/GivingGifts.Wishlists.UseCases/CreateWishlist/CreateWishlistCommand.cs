using Ardalis.Result;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.CreateWishlist;

public record CreateWishlistCommand(string? Name, CreateWishDto[] Wishes)
    : IRequest<Result<WishlistDto>>;