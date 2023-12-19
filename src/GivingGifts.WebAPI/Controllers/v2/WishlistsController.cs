using Ardalis.Result;
using Asp.Versioning;
using GivingGifts.SharedKernel.API.Enums;
using GivingGifts.SharedKernel.API.Extensions;
using GivingGifts.SharedKernel.API.Extensions.Result;
using GivingGifts.SharedKernel.API.Resources.FilterAttributes;
using GivingGifts.SharedKernel.API.Resources.Mapping;
using GivingGifts.WebAPI.Controllers.v2.Extensions;
using GivingGifts.Wishlists.API.ApiModels.V2;
using GivingGifts.Wishlists.API.ApiModels.V2.Mappers;
using GivingGifts.Wishlists.API.ApiModels.V2.Requests;
using GivingGifts.Wishlists.API.Constants;
using GivingGifts.Wishlists.Core.DTO;
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
    private readonly IResourceMapper _resourceMapper;
    private readonly IMediator _mediator;

    public WishlistsController(
        IResourceMapper resourceMapper,
        IMediator mediator)
    {
        _resourceMapper = resourceMapper;
        _mediator = mediator;
    }

    [HttpGet(Name = RouteNames.Wishlists.GetWishlistList)]
    [HttpHead]
    [ValidateResourcesRequest<WishlistDto, Wishlist>]
    public async Task<ActionResult> GetList(
        [FromQuery] WishlistsRequestBase requestBase)
    {
        var query = new WishlistsQuery(
            requestBase.Page,
            requestBase.PageSize,
            _resourceMapper.GetSortingParameters<WishlistDto, Wishlist>(requestBase));
        var result = await _mediator.Send(query);

        return result
            .Map(pr => pr.Map(_resourceMapper.Map<WishlistDto, Wishlist>))
            .ToPagedActionResult<WishlistDto, Wishlist>(
                this,
                requestBase,
                this.GenerateGetListResourceUrl(requestBase, result, ResourceUriType.PreviousPage),
                this.GenerateGetListResourceUrl(requestBase, result, ResourceUriType.NextPage));
    }

    [Route("{wishlistId:guid}", Name = RouteNames.Wishlists.GetWishlist)]
    [HttpGet]
    [HttpHead]
    [ValidateResourceRequest<WishlistWithWishes>]
    public async Task<ActionResult> Get(
        [FromRoute] Guid wishListId,
        [FromQuery] WishlistRequestBase requestBase)
    {
        var result = await _mediator.Send(new WishlistQuery(wishListId));
        return result
            .Map(WishlistWithWishesMapper.ToApiModel)
            .ToActionResult(requestBase, this);
    }

    [HttpPost]
    public async Task<ActionResult<Wishlist>> Create([FromBody] CreateWishlistRequest request)
    {
        var result = await _mediator.Send(new CreateWishlistCommand(
            request.Name,
            CreateWishRequestMapper.ToCommandDto(request.Wishes)));
        return result.Map(_resourceMapper.Map<WishlistDto, Wishlist>)
            .ToCreatedAtRouteActionResult(
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
        return this.OptionsActionResult(
            HttpMethod.Get, HttpMethod.Head, HttpMethod.Post);
    }

    [HttpOptions("{wishlistId:guid}")]
    public ActionResult OptionsConcrete()
    {
        return this.OptionsActionResult(
            HttpMethod.Get, HttpMethod.Head, HttpMethod.Delete, HttpMethod.Put);
    }
}