using Ardalis.Result.AspNetCore;
using GivingGifts.Wishlists.API.DTO;
using GivingGifts.Wishlists.UseCases;
using GivingGifts.Wishlists.UseCases.Create;
using GivingGifts.Wishlists.UseCases.Delete;
using GivingGifts.Wishlists.UseCases.GetList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.WebAPI.Controllers;

[ApiController]
[Route("api/wishlists")]
[Authorize]
public class WishlistsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WishlistsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WishlistDto>>> Get()
    {
        var result = await _mediator.Send(new UserWishlistsQuery());
        return this.ToActionResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<WishlistDto>> Create([FromBody] CreateWishlistDto request)
    {
        var result = await _mediator.Send(new CreateWishlistCommand(request.Name));
        return this.ToActionResult(result);
    }

    [HttpDelete("{wishlistId:guid}")]
    public async Task<ActionResult> Delete(Guid wishlistId)
    {
        var result = await _mediator.Send(new DeleteWishlistCommand(wishlistId));
        return this.ToActionResult(result);
    }
}