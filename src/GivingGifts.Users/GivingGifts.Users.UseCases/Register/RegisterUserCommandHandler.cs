using Ardalis.Result;
using GivingGifts.Users.Core;
using GivingGifts.Users.Core.Entities;
using GivingGifts.Users.Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace GivingGifts.Users.UseCases.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserCommandResult>>
{
    private readonly UserManager<User> _userManager;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IAuthTokenGenerator _authTokenGenerator;

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
        return identityResult.Succeeded
            ? Result<RegisterUserCommandResult>.Success(new(_authTokenGenerator.Generate(user).AuthToken))
            : Result<RegisterUserCommandResult>.Invalid(identityResult.AsErrors().ToList());
    }
}