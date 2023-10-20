using Ardalis.Result;
using GivingGifts.Users.Core;
using GivingGifts.Users.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GivingGifts.Users.UseCases.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginUserCommandResult>>
{
    private readonly IAuthTokenGenerator _authTokenGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(
        UserManager<User> userManager,
        IAuthTokenGenerator authTokenGenerator,
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _authTokenGenerator = authTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<Result<LoginUserCommandResult>> Handle(LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        if (user == null) return Result<LoginUserCommandResult>.Unauthorized();
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid) return Result<LoginUserCommandResult>.Unauthorized();

        var tokens = _authTokenGenerator.Generate(user);
        return Result<LoginUserCommandResult>.Success(new LoginUserCommandResult(tokens.AuthToken));
    }
}