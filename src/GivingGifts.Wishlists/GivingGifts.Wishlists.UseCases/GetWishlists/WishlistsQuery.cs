using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.DTO;
using MediatR;

namespace GivingGifts.Wishlists.UseCases.GetWishlists;

public record WishlistsQuery(int Page, int PageSize, SortingParameter[] SortingsParams)
    : IRequest<Result<PagedData<WishlistDto>>>;