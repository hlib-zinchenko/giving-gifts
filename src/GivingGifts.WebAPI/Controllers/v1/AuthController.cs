using System.Net;
using Ardalis.Result;
using Asp.Versioning;
using GivingGifts.SharedKernel.API.Extensions.Result;
using GivingGifts.SharedKernel.API.ResultStatusMapping;
using GivingGifts.Users.API.ApiModels;
using GivingGifts.Users.API.ApiModels.Mappers;
using GivingGifts.Users.UseCases.Login;
using GivingGifts.Users.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.WebAPI.Controllers.v1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [OkResultMappingOverride(HttpStatusCode.OK)]
    public async Task<ActionResult<AuthTokens>> Login([FromBody] LoginRequest request)
    {
        var result = await _mediator.Send(new LoginUserCommand(request.Email, request.Password));
        return result.Map(AuthTokensDtoMapper.ToApiModel).ToActionResult(this);
    }

    [HttpPost("register")]
    [OkResultMappingOverride(HttpStatusCode.OK)]
    public async Task<ActionResult<AuthTokens>> Register([FromBody] RegisterRequest request)
    {
        var result = await _mediator.Send(
            new RegisterUserCommand(request.FirstName, request.LastName, request.Email, request.Password));
        return result.Map(AuthTokensDtoMapper.ToApiModel).ToActionResult(this);
    }
}