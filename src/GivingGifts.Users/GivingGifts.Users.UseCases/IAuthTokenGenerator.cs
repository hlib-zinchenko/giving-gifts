using GivingGifts.Users.Core.Entities;
using GivingGifts.Users.Core.Models;

namespace GivingGifts.Users.UseCases;

public interface IAuthTokenGenerator
{
    AuthTokens Generate(User user);
}