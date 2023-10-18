using SharedKernel;

namespace GivingGifts.WebAPI.Auth;

public interface IUserContextResolver
{
    IUserContext Resolve();
    bool IsAuthenticated();
}