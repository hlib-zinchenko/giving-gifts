using Ardalis.Result.AspNetCore;
using Asp.Versioning;
using GivingGifts.Users.API.DTO;
using GivingGifts.Users.UseCases;
using GivingGifts.Users.UseCases.Login;
using GivingGifts.Users.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GivingGifts.WebAPI.Controllers.v2;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/auth")]
public class AuthControllerV2 : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthControllerV2(IMediator mediator)
    {
        _mediator = mediator;
    }

    [MapToApiVersion("2.0")]
    [HttpPost("login")]
    public async Task<ActionResult<AuthTokensDto>> Login([FromBody] LoginDto dto)
    {
        var result = await _mediator.Send(new LoginUserCommand(dto.Email, dto.Password));
        return this.ToActionResult(result);
    }

    [MapToApiVersion("2.0")]
    [HttpPost("register")]
    public async Task<ActionResult<AuthTokensDto>> Register([FromBody] RegisterDto dto)
    {
        var result = await _mediator.Send(
            new RegisterUserCommand(dto.FirstName, dto.LastName, dto.Email, dto.Password));
        return this.ToActionResult(result);
    }
}