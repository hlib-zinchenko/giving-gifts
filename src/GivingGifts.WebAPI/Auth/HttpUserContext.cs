﻿using GivingGifts.SharedKernel.Core;
using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.WebAPI.Auth;

public class HttpUserContext : IUserContext
{
    public HttpUserContext(Guid userId, string role)
    {
        if (!RoleNames.IsRolesExist(role)) throw new ArgumentException($"{role} does not exist");

        UserId = userId;
        Role = role;
    }

    public Guid UserId { get; }
    public string Role { get; }
}