using GivingGifts.Users.Core.Entities;
using GivingGifts.Users.Core.Models;

namespace GivingGifts.Users.Core;

public interface IAuthTokenGenerator
{
    AuthTokens Generate(User user);
}