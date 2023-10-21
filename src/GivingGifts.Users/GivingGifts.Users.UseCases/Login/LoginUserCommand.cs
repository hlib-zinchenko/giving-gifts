using Ardalis.Result;
using MediatR;

namespace GivingGifts.Users.UseCases.Login;

public record LoginUserCommand(string Email, string Password)
    : IRequest<Result<AuthTokensDto>>;