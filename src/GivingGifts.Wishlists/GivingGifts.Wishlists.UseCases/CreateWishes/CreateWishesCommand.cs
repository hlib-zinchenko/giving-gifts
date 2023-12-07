using Ardalis.Result;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.CreateWishes;

public record CreateWishesCommand(Guid WishlistId, CreateWishDto[] Wishes)
    : IRequest<Result<WishDto[]>>;