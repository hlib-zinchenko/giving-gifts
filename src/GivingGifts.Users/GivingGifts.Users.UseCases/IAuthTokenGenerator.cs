using GivingGifts.Users.Core.Entities;

namespace GivingGifts.Users.UseCases;

public interface IAuthTokenGenerator
{
    AuthTokensDto Generate(User user);
}