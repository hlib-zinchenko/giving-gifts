using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Asp.Versioning;
using GivingGifts.SharedKernel.API.Extensions;
using GivingGifts.SharedKernel.API.Extensions.Result;
using GivingGifts.SharedKernel.API.Models;
using GivingGifts.WebAPI.Controllers.v2.Extensions;
using GivingGifts.Wishlists.API.ApiModels.V2;
using GivingGifts.Wishlists.API.ApiModels.V2.Mappers;
using GivingGifts.Wishlists.API.Constants;
using GivingGifts.Wishlists.UseCases.CreateWishlist;
using GivingGifts.Wishlists.UseCases.DeleteWishlist;
using GivingGifts.Wishlists.UseCases.GetWishlist;
using GivingGifts.Wishlists.UseCases.GetWishlists;
using GivingGifts.Wishlists.UseCases.UpdateWishlist;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.WebAPI.Controllers.v2;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/wishlists")]
[Authorize]
[TranslateResultToActionResult]
public class WishlistsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WishlistsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = RouteNames.Wishlists.GetWishlistList)]
    [HttpHead]
    public async Task<ActionResult<Wishlist[]>> GetList(
        [FromQuery] WishlistsRequest request)
    {
        var query = new WishlistsQuery(request.Page, request.PageSize);
        var result = await _mediator.Send(query);
        return result.Map(pr => pr.Map(WishlistMapper.ToApiModel))
            .ToPagedActionResult(
                this,
                this.GenerateGetListResourceUrl(request, result, ResourceUriType.PreviousPage),
                this.GenerateGetListResourceUrl(request, result, ResourceUriType.NextPage));
    }

    [Route("{wishlistId:guid}", Name = RouteNames.Wishlists.GetWishlist)]
    [HttpGet]
    [HttpHead]
    public async Task<Result<WishlistWithWishes>> Get([FromRoute] Guid wishListId)
    {
        var result = await _mediator.Send(new WishlistQuery(wishListId));
        return result.Map(WishlistWithWishesMapper.ToApiModel);
    }

    [HttpPost]
    public async Task<ActionResult<Wishlist>> Create([FromBody] CreateWishlistRequest request)
    {
        var result = await _mediator.Send(new CreateWishlistCommand(
            request.Name,
            CreateWishRequestMapper.ToCommandDto(request.Wishes)));
        return result.Map(WishlistMapper.ToApiModel).ToCreatedAtRouteActionResult(
            this,
            RouteNames.Wishlists.GetWishlist,
            new { wishListId = result.Value?.Id });
    }

    [HttpDelete("{wishlistId:guid}")]
    public async Task<Result> Delete(Guid wishlistId)
    {
        var result = await _mediator.Send(new DeleteWishlistCommand(wishlistId));
        return result;
    }

    [HttpPut("{wishlistId:guid}")]
    public async Task<Result> Update(Guid wishlistId, [FromBody] UpdateWishlistRequest request)
    {
        var result = await _mediator.Send(new UpdateWishlistCommand(wishlistId, request.Name));
        return result;
    }

    [HttpOptions]
    public ActionResult Options()
    {
        return this.OptionsActionResult("GET", "HEAD", "POST");
    }

    [HttpOptions("{wishlistId:guid}")]
    public ActionResult OptionsConcrete()
    {
        return this.OptionsActionResult("GET", "HEAD", "DELETE", "PUT");
    }
}