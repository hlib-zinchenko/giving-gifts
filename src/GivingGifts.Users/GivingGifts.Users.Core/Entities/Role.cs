using Microsoft.AspNetCore.Identity;

namespace GivingGifts.Users.Core.Entities;

public sealed class Role : IdentityRole<Guid>
{
    public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();

    // ReSharper disable once UnusedMember.Local
    private Role()
    {
        
    }
    public Role(Guid id, string name)
    {
        Id = id;
        Name = name;
        NormalizedName = name.ToUpper();
    }
}