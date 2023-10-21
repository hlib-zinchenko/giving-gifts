using GivingGifts.SharedKernel.Core;

namespace GivingGifts.WebAPI.Auth;

public class HttpUserContextResolver : IUserContextResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUserContextResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IUserContext Resolve()
    {
        var context = _httpContextAccessor.HttpContext;
        var principal = context?.User;

        if (principal == null || principal.Identity is { IsAuthenticated: false })
            throw new Exception("User is not authenticated");

        var userId = Guid.Parse(principal.Claims.First(x => x.Type == ClaimNames.UserId).Value);
        var role = principal.Claims.First(x => x.Type == ClaimNames.Role).Value;
        var user = new HttpUserContext(userId, role);

        return user;
    }

    public bool IsAuthenticated()
    {
        var context = _httpContextAccessor.HttpContext;
        var principal = context?.User;

        return principal is { Identity: not { IsAuthenticated: false } };
    }
}