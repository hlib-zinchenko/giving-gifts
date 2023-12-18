using Ardalis.Result;
using GivingGifts.SharedKernel.API.Enums;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.API.ApiModels.V2.Requests;
using GivingGifts.Wishlists.API.Constants;

namespace GivingGifts.WebAPI.Controllers.v2.Extensions;

public static class WishlistsControllerExtensions
{
    public static string? GenerateGetListResourceUrl<T>(
        this WishlistsController wishlistsController,
        WishlistsRequest request,
        Result<PagedData<T>> result,
        ResourceUriType uriType)
    {
        if (result.Status != ResultStatus.Ok)
        {
            return null;
        }

        switch (uriType)
        {
            case ResourceUriType.PreviousPage:
            {
                return result.Value.HasPrevious
                    ? wishlistsController.Url.Link(RouteNames.Wishlists.GetWishlistList, new
                    {
                        Page = request.Page - 1,
                        request.PageSize,
                    })
                    : null;
            }
            case ResourceUriType.NextPage:
            {
                return result.Value.HasNext
                    ? wishlistsController.Url.Link(RouteNames.Wishlists.GetWishlistList, new
                    {
                        Page = request.Page + 1,
                        request.PageSize,
                    })
                    : null;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(uriType), uriType, null);
        }
    }
}