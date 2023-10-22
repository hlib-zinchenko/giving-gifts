using Ardalis.Result;
using GivingGifts.SharedKernel.Core.Attributes;
using MediatR;

namespace GivingGifts.Users.UseCases.Login;

public record LoginUserCommand
    : IRequest<Result<AuthTokensDto>>
{
    [SensitiveData]
    public string Email { get; }
    public string Password { get; }

    public LoginUserCommand(string? email, string? password)
    {
        Email = email!;
        Password = password!;
    }
}