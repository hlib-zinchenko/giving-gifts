using Ardalis.Result;
using Asp.Versioning;
using GivingGifts.SharedKernel.API.Extensions.Result;
using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.Wishlists.API.ApiModels.V1;
using GivingGifts.Wishlists.API.ApiModels.V1.Mappers;
using GivingGifts.Wishlists.UseCases.DeleteWishlist;
using GivingGifts.Wishlists.UseCases.GetWishlists;
using GivingGifts.Wishlists.UseCases.UpdateWishlist;
using GivingGifts.Wishlists.UseCases.V1.CreateWishlist;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishlistDto = GivingGifts.Wishlists.API.ApiModels.V1.WishlistDto;

namespace GivingGifts.WebAPI.Controllers.v1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/wishlists")]
[Authorize]
public class WishlistsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WishlistsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<WishlistDto[]>> Get()
    {
        var result = await _mediator.Send(new WishlistsQuery(
            Paging.DefaultPage,
            int.MaxValue));
        return result
            .Map(r => WishlistDtoMapper.ToApiDto(r.Data))
            .ToActionResult(this);
    }

    [HttpPost]
    public async Task<ActionResult<WishlistDto>> Create([FromBody] CreateWishlistDto request)
    {
        var result = await _mediator.Send(new CreateWishlistCommand(request.Name));
        return result
            .Map(WishlistDtoMapper.ToApiDto)
            .ToActionResult(this);
    }

    [HttpDelete("{wishlistId:guid}")]
    public async Task<ActionResult> Delete(Guid wishlistId)
    {
        var result = await _mediator.Send(new DeleteWishlistCommand(wishlistId));
        return result
            .ToActionResult(this);
    }

    [HttpPut("{wishlistId:guid}")]
    public async Task<ActionResult> Update(Guid wishlistId, [FromBody] UpdateWishlistDto request)
    {
        var result = await _mediator.Send(new UpdateWishlistCommand(wishlistId, request.Name));
        return result
            .ToActionResult(this);
    }
}