using Ardalis.Result;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishes;

public record WishesQuery(Guid wishlistId) : IRequest<Result<IEnumerable<WishDto>>>;