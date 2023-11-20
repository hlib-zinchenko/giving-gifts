using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Asp.Versioning;
using GivingGifts.Wishlists.API.DTO;
using GivingGifts.Wishlists.UseCases;
using GivingGifts.Wishlists.UseCases.CreateWish;
using GivingGifts.Wishlists.UseCases.DeleteWish;
using GivingGifts.Wishlists.UseCases.GetWish;
using GivingGifts.Wishlists.UseCases.GetWishes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.WebAPI.Controllers.v2;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/wishlists/{wishlistId:guid}/wishes")]
[Authorize]
[TranslateResultToActionResult]
public class WishesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WishesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [HttpHead]
    public async Task<Result<IEnumerable<WishDto>>> Get(Guid wishlistId)
    {
        var result = await _mediator.Send(new WishesQuery(wishlistId));
        return result;
    }

    [Route("{wishId:guid}")]
    [HttpGet]
    [HttpHead]
    public async Task<Result<WishDto>> Get(Guid wishId, Guid wishlistId)
    {
        var result = await _mediator.Send(new WishQuery(wishId, wishlistId));
        return result;
    }

    [HttpPost]
    public async Task<Result<WishDto>> Create(
        [FromBody] CreateWishDto request,
        Guid wishlistId)
    {
        var result = await _mediator.Send(new CreateWishCommand(
            wishlistId, request.Name!, request.Url));
        return result;
    }

    [HttpDelete("{wishId:guid}")]
    public async Task<Result> Delete(Guid wishlistId, Guid wishId)
    {
        var result = await _mediator.Send(new DeleteWishCommand(wishlistId, wishId));
        return result;
    }
}