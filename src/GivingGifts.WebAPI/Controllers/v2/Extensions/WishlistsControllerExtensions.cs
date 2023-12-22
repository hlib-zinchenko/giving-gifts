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
        WishlistsRequestBase requestBase,
        Result<PagedData<T>> result,
        ResourceUriType uriType)
    {
        if (result.Status != ResultStatus.Ok)
        {
            return null;
        }

        return uriType switch
        {
            ResourceUriType.PreviousPage => result.Value.HasPrevious
                ? wishlistsController.Url.Link(RouteNames.Wishlists.GetWishlistList,
                    new { Page = requestBase.Page - 1, requestBase.PageSize, })
                : null,
            ResourceUriType.NextPage => result.Value.HasNext
                ? wishlistsController.Url.Link(RouteNames.Wishlists.GetWishlistList,
                    new { Page = requestBase.Page + 1, requestBase.PageSize, })
                : null,
            _ => throw new ArgumentOutOfRangeException(nameof(uriType), uriType, null)
        };
    }
}