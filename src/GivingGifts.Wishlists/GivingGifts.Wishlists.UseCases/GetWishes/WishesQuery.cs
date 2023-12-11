using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishes;

public record WishesQuery(Guid WishlistId, int Page, int PageSize)
    : IRequest<Result<PagedData<WishDto>>>;