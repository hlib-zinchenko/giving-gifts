using GivingGifts.Users.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GivingGifts.Users.Infrastructure.Data;

public class UsersDbContext  : IdentityDbContext<User, Role, Guid>
{
    
}