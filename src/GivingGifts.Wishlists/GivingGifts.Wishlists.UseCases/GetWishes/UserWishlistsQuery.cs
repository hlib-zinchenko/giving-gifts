using Ardalis.Result;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishes;

public record WishesQuery(Guid WishlistId, Guid[]? WishIds = null) : IRequest<Result<IEnumerable<WishDto>>>;