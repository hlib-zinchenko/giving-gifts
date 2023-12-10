using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Asp.Versioning;
using GivingGifts.SharedKernel.API.Extensions;
using GivingGifts.SharedKernel.API.Extensions.Result;
using GivingGifts.SharedKernel.API.ModelBinders;
using GivingGifts.Wishlists.API.DTO.V2.Mappers;
using GivingGifts.Wishlists.UseCases.CreateWishes;
using GivingGifts.Wishlists.UseCases.GetWishes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CreateWishDto = GivingGifts.Wishlists.API.DTO.V2.CreateWishDto;
using WishDto = GivingGifts.Wishlists.API.DTO.V2.WishDto;

namespace GivingGifts.WebAPI.Controllers.v2;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/wishlists/{wishlistId:guid}/wish-collections")]
[Authorize]
[TranslateResultToActionResult]
public class WishCollectionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WishCollectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{wishIds}", Name = "GetWishCollection")]
    [HttpHead]
    public async Task<Result<WishDto[]>> Get(
        [FromRoute] Guid wishlistId,
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] [FromRoute]
        Guid[] wishIds)
    {
        var query = new WishesQuery(wishlistId, wishIds);
        var result = await _mediator.Send(query);
        return result.Map(WishDtoMapper.ToApiDto);
    }

    [HttpPost]
    public async Task<ActionResult<WishDto[]>> Create(
        [FromRoute] Guid wishlistId,
        [FromBody] CreateWishDto[] wishes)
    {
        var command = new CreateWishesCommand(
            wishlistId,
            wishes
                .Select(w => new Wishlists.Core.DTO.CreateWishDto(w.Name, w.Url, w.Notes))
                .ToArray()
        );
        var result = await _mediator.Send(command);
        var wishIdsAString = string.Join(",",
            result.Value?.Select(w => w.Id).ToArray() ?? Array.Empty<Guid>());
        return result.Map(WishDtoMapper.ToApiDto).ToCreatedAtRouteActionResult(
            this,
            "GetWishCollection",
            new { wishlistId, wishIds = wishIdsAString });
    }

    [HttpOptions]
    public ActionResult Options()
    {
        return this.OptionsActionResult("POST");
    }

    [HttpOptions("{wishIds}")]
    public ActionResult OptionsConcrete()
    {
        return this.OptionsActionResult("GET", "HEAD");
    }
}