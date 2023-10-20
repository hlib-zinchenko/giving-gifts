using Ardalis.Result;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Users.Core.Entities;
using GivingGifts.Users.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GivingGifts.Users.UseCases.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserCommandResult>>
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

    public async Task<Result<RegisterUserCommandResult>> Handle(
        RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(_dateTimeProvider, request.FirsName, request.LastName, request.Email);
        var identityResult = await _userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
        {
            return Result<RegisterUserCommandResult>.Invalid(identityResult.AsErrors().ToList());
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(user, RoleNames.User);

        return addToRoleResult.Succeeded
            ? Result<RegisterUserCommandResult>.Success(
                new RegisterUserCommandResult(_authTokenGenerator.Generate(user).AuthToken))
            : Result<RegisterUserCommandResult>.Invalid(addToRoleResult.AsErrors().ToList());
    }
}