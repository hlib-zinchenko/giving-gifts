using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Asp.Versioning;
using GivingGifts.Wishlists.API.DTO;
using GivingGifts.Wishlists.UseCases;
using GivingGifts.Wishlists.UseCases.CreateWishlist;
using GivingGifts.Wishlists.UseCases.DeleteWishlist;
using GivingGifts.Wishlists.UseCases.GetUserWishlists;
using GivingGifts.Wishlists.UseCases.GetWishlist;
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

    [HttpGet]
    [HttpHead]
    public async Task<Result<IEnumerable<WishlistDto>>> GetList()
    {
        var result = await _mediator.Send(new UserWishlistsQuery());
        return result;
    }
    
    [Route("{wishlistId:guid}")]
    [HttpGet]
    [HttpHead]
    public async Task<Result<WishlistWithWishesDto>> Get(Guid wishListId)
    {
        var result = await _mediator.Send(new WishlistQuery(wishListId));
        return result;
    }

    [HttpPost]
    public async Task<Result<WishlistDto>> Create([FromBody] CreateWishlistDto request)
    {
        var result = await _mediator.Send(new CreateWishlistCommand(request.Name));
        return result;
    }

    [HttpDelete("{wishlistId:guid}")]
    public async Task<Result> Delete(Guid wishlistId)
    {
        var result = await _mediator.Send(new DeleteWishlistCommand(wishlistId));
        return result;
    }

    [HttpPut("{wishlistId:guid}")]
    public async Task<Result> Update(Guid wishlistId, [FromBody] UpdateWishlistDto request)
    {
        var result = await _mediator.Send(new UpdateWishlistCommand(wishlistId, request.Name));
        return result;
    }
}