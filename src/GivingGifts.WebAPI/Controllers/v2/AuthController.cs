using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Asp.Versioning;
using GivingGifts.SharedKernel.API.Extensions;
using GivingGifts.Users.API.ApiModels;
using GivingGifts.Users.API.ApiModels.Mappers;
using GivingGifts.Users.UseCases.Login;
using GivingGifts.Users.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.WebAPI.Controllers.v2;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/auth")]
[TranslateResultToActionResult]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpOptions]
    public ActionResult Options()
    {
        return this.OptionsActionResult("POST");
    }

    [HttpPost("login")]
    public async Task<Result<AuthTokens>> Login([FromBody] LoginRequest request)
    {
        var result = await _mediator.Send(new LoginUserCommand(request.Email, request.Password));
        return result.Map(AuthTokensDtoMapper.ToApiModel);
    }

    [HttpPost("register")]
    public async Task<Result<AuthTokens>> Register([FromBody] RegisterRequest request)
    {
        var result = await _mediator.Send(
            new RegisterUserCommand(request.FirstName, request.LastName, request.Email, request.Password));
        return result.Map(AuthTokensDtoMapper.ToApiModel);
    }
}