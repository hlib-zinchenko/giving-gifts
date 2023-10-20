using GivingGifts.SharedKernel.Core;

namespace GivingGifts.WebAPI.Auth;

public interface IUserContextResolver
{
    IUserContext Resolve();
    bool IsAuthenticated();
}