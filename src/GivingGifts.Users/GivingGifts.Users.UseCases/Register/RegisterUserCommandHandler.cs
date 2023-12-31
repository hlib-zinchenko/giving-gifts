using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.SharedKernel.Core.Constants;
using GivingGifts.Users.Core.Entities;
using GivingGifts.Users.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GivingGifts.Users.UseCases.Register;

public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, Result<AuthTokensDto>>
{
    private readonly IAuthTokenGenerator _authTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly UserManager<User> _userManager;

    public RegisterUserCommandHandler(
        UserManager<User> userManager,
        IDateTimeProvider dateTimeProvider,
        IAuthTokenGenerator authTokenGenerator)
    {
        _userManager = userManager;
        _dateTimeProvider = dateTimeProvider;
        _authTokenGenerator = authTokenGenerator;
    }

    public async Task<Result<AuthTokensDto>> Handle(
        RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(_dateTimeProvider, request.FirsName, request.LastName, request.Email);
        var identityResult = await _userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
        {
            return Result<AuthTokensDto>.Invalid(identityResult.AsErrors().ToList());
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, RoleNames.User);

        return addToRoleResult.Succeeded
            ? Result<AuthTokensDto>.Success(
                _authTokenGenerator.Generate(user))
            : Result<AuthTokensDto>.Invalid(addToRoleResult.AsErrors().ToList());
    }
}