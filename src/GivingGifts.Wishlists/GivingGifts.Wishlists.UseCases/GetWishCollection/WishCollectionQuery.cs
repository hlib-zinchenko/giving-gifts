using Ardalis.Result;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishCollection;

public record WishCollectionQuery(Guid WishlistId, Guid[] WishIds)
    : IRequest<Result<IEnumerable<WishDto>>>;