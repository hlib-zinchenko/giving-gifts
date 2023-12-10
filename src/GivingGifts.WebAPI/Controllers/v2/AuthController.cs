using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Asp.Versioning;
using GivingGifts.SharedKernel.API.Extensions;
using GivingGifts.Users.API.DTO;
using GivingGifts.Users.API.DTO.Mappers;
using GivingGifts.Users.UseCases.Login;
using GivingGifts.Users.UseCases.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuthTokensDto = GivingGifts.Users.API.DTO.AuthTokensDto;

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
    public async Task<Result<AuthTokensDto>> Login([FromBody] LoginDto dto)
    {
        var result = await _mediator.Send(new LoginUserCommand(dto.Email, dto.Password));
        return result.Map(AuthTokensDtoMapper.ToApiDto);
    }

    [HttpPost("register")]
    public async Task<Result<AuthTokensDto>> Register([FromBody] RegisterDto dto)
    {
        var result = await _mediator.Send(
            new RegisterUserCommand(dto.FirstName, dto.LastName, dto.Email, dto.Password));
        return result.Map(AuthTokensDtoMapper.ToApiDto);
    }
}