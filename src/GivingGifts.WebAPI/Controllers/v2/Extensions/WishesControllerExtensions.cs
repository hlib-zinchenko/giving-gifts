using Ardalis.Result;
using GivingGifts.SharedKernel.API.Models;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.API.ApiModels.V2.Requests;
using GivingGifts.Wishlists.API.Constants;

namespace GivingGifts.WebAPI.Controllers.v2.Extensions;

public static class WishesControllerExtensions
{
    public static string? GenerateGetListResourceUrl<T>(
        this WishesController wishlistsController,
        Guid wishlistId,
        WishesRequest request,
        Result<PagedData<T>> result,
        ResourceUriType uriType)
    {
        if (result.Status != ResultStatus.Ok)
        {
            return null;
        }

        const string routeName = RouteNames.Wishlists.GetWishlistList;
        switch (uriType)
        {
            case ResourceUriType.PreviousPage:
            {
                return result.Value.HasPrevious
                    ? wishlistsController.Url.Link(routeName, new
                    {
                        Page = request.Page - 1,
                        request.PageSize,
                        wishlistId,
                    })
                    : null;
            }
            case ResourceUriType.NextPage:
            {
                return result.Value.HasNext
                    ? wishlistsController.Url.Link(routeName, new
                    {
                        Page = request.Page + 1,
                        request.PageSize,
                        wishlistId,
                    })
                    : null;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(uriType), uriType, null);
        }
    }
}