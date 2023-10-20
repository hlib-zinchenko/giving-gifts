using Microsoft.AspNetCore.Identity;

namespace GivingGifts.Users.Core.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public Role Role { get; set; } = null!;
    public User User { get; set; } = null!;
}