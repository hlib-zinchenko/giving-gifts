using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Asp.Versioning;
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
    public async Task<Result<WishlistDto[]>> Get()
    {
        var result = await _mediator.Send(new WishlistsQuery(
            Paging.DefaultPage,
            int.MaxValue));
        return result.Map(r => WishlistDtoMapper.ToApiDto(r.Data));
    }

    [HttpPost]
    public async Task<Result<WishlistDto>> Create([FromBody] CreateWishlistDto request)
    {
        var result = await _mediator.Send(new CreateWishlistCommand(request.Name));
        return result.Map(WishlistDtoMapper.ToApiDto);
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