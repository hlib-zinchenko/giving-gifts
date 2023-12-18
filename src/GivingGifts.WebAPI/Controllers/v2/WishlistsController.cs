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
    [ValidateDataShaping]
    [ValidateSorting<WishlistDto, Wishlist>]
    public async Task<ActionResult> GetList(
        [FromQuery] WishlistsRequest request)
    {
        var query = new WishlistsQuery(
            request.Page,
            request.PageSize,
            _resourceMapper.GetSortingParameters<WishlistDto, Wishlist>(request));
        var result = await _mediator.Send(query);

        return result
            .Map(pr => pr.Map(_resourceMapper.Map<WishlistDto, Wishlist>))
            .Shape(request)
            .ToPagedActionResult(
                this,
                this.GenerateGetListResourceUrl(request, result, ResourceUriType.PreviousPage),
                this.GenerateGetListResourceUrl(request, result, ResourceUriType.NextPage));
    }

    [Route("{wishlistId:guid}", Name = RouteNames.Wishlists.GetWishlist)]
    [HttpGet]
    [HttpHead]
    [ValidateDataShaping]
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
        return result.Map(_resourceMapper.Map<WishlistDto, Wishlist>).ToCreatedAtRouteActionResult(
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