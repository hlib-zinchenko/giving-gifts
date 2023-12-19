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
using GivingGifts.Wishlists.UseCases.CreateWish;
using GivingGifts.Wishlists.UseCases.DeleteWish;
using GivingGifts.Wishlists.UseCases.GetWish;
using GivingGifts.Wishlists.UseCases.GetWishes;
using GivingGifts.Wishlists.UseCases.UpdateWish;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.WebAPI.Controllers.v2;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/wishlists/{wishlistId:guid}/wishes")]
[Authorize]
public class WishesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IResourceMapper _resourceMapper;

    public WishesController(
        IMediator mediator,
        IResourceMapper resourceMapper)
    {
        _mediator = mediator;
        _resourceMapper = resourceMapper;
    }

    [HttpGet(Name = RouteNames.Wishes.GetWishesList)]
    [HttpHead]
    [ValidateResourcesRequest<WishDto, Wish>]
    public async Task<ActionResult> GetList(
        [FromRoute] Guid wishlistId,
        [FromQuery] WishesRequestBase requestBase)
    {
        var query = new WishesQuery(
            wishlistId,
            requestBase.Page,
            requestBase.PageSize,
            _resourceMapper.GetSortingParameters<WishDto, Wish>(requestBase));

        var result = await _mediator.Send(query);
        return result
            .Map(pr => pr.Map(_resourceMapper.Map<WishDto, Wish>))
            .ToPagedActionResult<WishDto, Wish>(
                this,
                requestBase,
                this.GenerateGetListResourceUrl(wishlistId, requestBase, result, ResourceUriType.PreviousPage),
                this.GenerateGetListResourceUrl(wishlistId, requestBase, result, ResourceUriType.NextPage));
    }

    [Route("{wishId:guid}", Name = RouteNames.Wishes.GetWish)]
    [HttpGet]
    [HttpHead]
    [ValidateResourceRequest<Wish>]
    public async Task<ActionResult> Get(
        Guid wishId,
        Guid wishlistId,
        [FromQuery] WishRequestBase requestBase)
    {
        var result = await _mediator.Send(new WishQuery(wishId, wishlistId));
        return result
            .Map(_resourceMapper.Map<WishDto, Wish>)
            .ToActionResult(requestBase,this);
    }

    [HttpPost]
    public async Task<ActionResult<Wish>> Create(
        [FromBody] CreateWishRequest request,
        Guid wishlistId)
    {
        var result = await _mediator.Send(new CreateWishCommand(
            wishlistId, request.Name!, request.Url, request.Notes));
        return result.Map(_resourceMapper.Map<WishDto, Wish>).ToCreatedAtRouteActionResult(
            this,
            RouteNames.Wishes.GetWish,
            new { wishlistId, wishId = result.Value?.Id });
    }

    [HttpDelete("{wishId:guid}")]
    public async Task<ActionResult> Delete(Guid wishlistId, Guid wishId)
    {
        var result = await _mediator.Send(new DeleteWishCommand(wishlistId, wishId));
        return result
            .ToActionResult(this);
    }

    [HttpPut("{wishId:guid}")]
    public async Task<ActionResult<Wish>> Update(
        [FromBody] UpdateWishRequest request,
        Guid wishlistId,
        Guid wishId)
    {
        var result = await _mediator.Send(new UpdateWishCommand(
            wishlistId, wishId, request.Name!, request.Url, request.Notes));
        return result
            .ToActionResult(this);
    }

    [HttpPatch("{wishId:guid}")]
    public async Task<ActionResult<Wish>> UpdatePartial(
        [FromBody] JsonPatchDocument<UpdateWishRequest> request,
        Guid wishlistId,
        Guid wishId)
    {
        var wishQueryResult = await _mediator.Send(new WishQuery(wishId, wishlistId));
        if (wishQueryResult.Status != ResultStatus.Ok)
        {
            return wishQueryResult
                .Map(_resourceMapper.Map<WishDto, Wish>)
                .ToActionResult(this);
        }

        var updateWishDto = WishDtoMapper.ToUpdateWishRequest(wishQueryResult.Value);
        request.ApplyTo(updateWishDto);
        var result = await _mediator.Send(new UpdateWishCommand(
            wishlistId, wishId, updateWishDto.Name!, updateWishDto.Url, updateWishDto.Notes));
        return result.ToActionResult(this);
    }

    [HttpOptions]
    public ActionResult Options()
    {
        return this.OptionsActionResult(
            HttpMethod.Get, HttpMethod.Head, HttpMethod.Post);
    }

    [HttpOptions("{wishId:guid}")]
    public ActionResult OptionsConcrete()
    {
        return this.OptionsActionResult(
            HttpMethod.Get,
            HttpMethod.Head,
            HttpMethod.Delete,
            HttpMethod.Put,
            HttpMethod.Patch);
    }
}