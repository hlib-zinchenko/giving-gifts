using Ardalis.Result;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.CreateWish;

public record CreateWishCommand(Guid WishlistId, string? Name, string? Url, string? Notes)
    : IRequest<Result<WishDto>>;