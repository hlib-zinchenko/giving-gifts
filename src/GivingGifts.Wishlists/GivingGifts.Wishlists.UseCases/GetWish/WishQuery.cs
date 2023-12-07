using Ardalis.Result;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWish;

public record WishQuery(Guid WishId, Guid WishlistId) : IRequest<Result<WishDto>>;