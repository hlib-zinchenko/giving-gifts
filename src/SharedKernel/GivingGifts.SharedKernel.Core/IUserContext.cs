using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.SharedKernel.Core;

public interface IUserContext
{
    Guid UserId { get; }
    string Role { get; }

    bool IsAdmin()
    {
        return Role == RoleNames.Admin;
    }

    bool IsUser()
    {
        return Role == RoleNames.User;
    }
}