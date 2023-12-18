using Ardalis.Result;
using Asp.Versioning;
using GivingGifts.SharedKernel.API.Extensions;
using GivingGifts.SharedKernel.API.Extensions.Result;
using GivingGifts.SharedKernel.API.ModelBinders;
using GivingGifts.SharedKernel.API.Resources.FilterAttributes;
using GivingGifts.SharedKernel.API.Resources.Mapping;
using GivingGifts.Wishlists.API.ApiModels.V2;
using GivingGifts.Wishlists.API.ApiModels.V2.Requests;
using GivingGifts.Wishlists.API.Constants;
using GivingGifts.Wishlists.Core.DTO;
using GivingGifts.Wishlists.UseCases.CreateWishes;
using GivingGifts.Wishlists.UseCases.GetWishCollection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.WebAPI.Controllers.v2;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/wishlists/{wishlistId:guid}/wish-collections")]
[Authorize]
public class WishCollectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IResourceMapper _resourceMapper;

    public WishCollectionsController(
        IMediator mediator,
        IResourceMapper resourceMapper)
    {
        _mediator = mediator;
        _resourceMapper = resourceMapper;
    }

    [HttpGet("{wishIds}", Name = RouteNames.WishCollections.GetWishCollection)]
    [HttpHead]
    [ValidateDataShaping<Wish>]
    public async Task<ActionResult> Get(
        [FromRoute] Guid wishlistId,
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] [FromRoute]
        Guid[] wishIds,
        [FromQuery] WishCollectionRequestBase requestBase)
    {
        var query = new WishCollectionQuery(wishlistId, wishIds);
        var result = await _mediator.Send(query);
        return result
            .Map(_resourceMapper.Map<WishDto, Wish>)
            .ToActionResult(requestBase, this);
    }

    [HttpPost]
    public async Task<ActionResult> Create(
        [FromRoute] Guid wishlistId,
        [FromBody] CreateWishRequest[] wishes)
    {
        var command = new CreateWishesCommand(
            wishlistId,
            wishes
                .Select(w => new CreateWishDto(w.Name, w.Url, w.Notes))
                .ToArray() 
        );
        var result = await _mediator.Send(command);
        var wishIdsAString = string.Join(",",
            result.Value?.Select(w => w.Id).ToArray() ?? Array.Empty<Guid>());
        return result
            .Map(_resourceMapper.Map<WishDto, Wish>)
            .ToCreatedAtRouteActionResult(
            this,
            RouteNames.WishCollections.GetWishCollection,
            new { wishlistId, wishIds = wishIdsAString });
    }

    [HttpOptions]
    public ActionResult Options()
    {
        return this.OptionsActionResult(HttpMethod.Post);
    }

    [HttpOptions("{wishIds}")]
    public ActionResult OptionsConcrete()
    {
        return this.OptionsActionResult(HttpMethod.Get, HttpMethod.Head);
    }
}