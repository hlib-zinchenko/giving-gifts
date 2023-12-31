﻿using GivingGifts.SharedKernel.Core.Constants;

namespace GivingGifts.SharedKernel.Core;

public interface IUserContext
{
    Guid UserId { get; }
    string Role { get; }
}