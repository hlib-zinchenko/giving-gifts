using Ardalis.Result;
using MediatR;

namespace GivingGifts.Users.UseCases.Register;

public class RegisterUserCommand : IRequest<Result<AuthTokensDto>>
{
    public RegisterUserCommand(string firsName, string lastName, string email, string password)
    {
        FirsName = firsName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public string FirsName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Password { get; }
}