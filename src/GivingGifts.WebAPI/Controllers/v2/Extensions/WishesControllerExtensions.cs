using Ardalis.Result;
using GivingGifts.SharedKernel.API.Enums;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.API.ApiModels.V2.Requests;
using GivingGifts.Wishlists.API.Constants;

namespace GivingGifts.WebAPI.Controllers.v2.Extensions;

public static class WishesControllerExtensions
{
    public static string? GenerateGetListResourceUrl<T>(
        this WishesController wishlistsController,
        Guid wishlistId,
        WishesRequestBase requestBase,
        Result<PagedData<T>> result,
        ResourceUriType uriType)
    {
        if (result.Status != ResultStatus.Ok)
        {
            return null;
        }

        const string routeName = RouteNames.Wishlists.GetWishlistList;
        return uriType switch
        {
            ResourceUriType.PreviousPage => result.Value.HasPrevious
                ? wishlistsController.Url.Link(routeName,
                    new { Page = requestBase.Page - 1, requestBase.PageSize, wishlistId, })
                : null,
            ResourceUriType.NextPage => result.Value.HasNext
                ? wishlistsController.Url.Link(routeName,
                    new { Page = requestBase.Page + 1, requestBase.PageSize, wishlistId, })
                : null,
            _ => throw new ArgumentOutOfRangeException(nameof(uriType), uriType, null)
        };
    }
}