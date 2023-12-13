using Ardalis.Result;
using Asp.Versioning;
using GivingGifts.SharedKernel.API.Extensions;
using GivingGifts.SharedKernel.API.Extensions.Result;
using GivingGifts.SharedKernel.API.Middleware;
using GivingGifts.SharedKernel.API.Models;
using GivingGifts.WebAPI.Controllers.v2.Extensions;
using GivingGifts.Wishlists.API.ApiModels.V2;
using GivingGifts.Wishlists.API.ApiModels.V2.Mappers;
using GivingGifts.Wishlists.API.ApiModels.V2.Requests;
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
public class WishlistsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WishlistsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = RouteNames.Wishlists.GetWishlistList)]
    [HttpHead]
    [ServiceFilter(typeof(ValidateDataShapingFilterAttribute))]
    public async Task<ActionResult> GetList(
        [FromQuery] WishlistsRequest request)
    {
        var query = new WishlistsQuery(request.Page, request.PageSize);
        var result = await _mediator.Send(query);
        return result
            .Map(pr => pr.Map(WishlistMapper.ToApiModel))
            .Shape(request)
            .ToPagedActionResult(
                this,
                this.GenerateGetListResourceUrl(request, result, ResourceUriType.PreviousPage),
                this.GenerateGetListResourceUrl(request, result, ResourceUriType.NextPage));
    }

    [Route("{wishlistId:guid}", Name = RouteNames.Wishlists.GetWishlist)]
    [HttpGet]
    [HttpHead]
    public async Task<ActionResult> Get(
        [FromRoute] Guid wishListId,
        [FromQuery] WishlistRequest request)
    {
        var result = await _mediator.Send(new WishlistQuery(wishListId));
        return result
            .Map(WishlistWithWishesMapper.ToApiModel)
            .Shape(request)
            .ToActionResult(this);
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
    public async Task<ActionResult> Delete(Guid wishlistId)
    {
        var result = await _mediator.Send(new DeleteWishlistCommand(wishlistId));
        return result.ToActionResult(this);
    }

    [HttpPut("{wishlistId:guid}")]
    public async Task<ActionResult> Update(Guid wishlistId, [FromBody] UpdateWishlistRequest request)
    {
        var result = await _mediator.Send(new UpdateWishlistCommand(wishlistId, request.Name));
        return result.ToActionResult(this);
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