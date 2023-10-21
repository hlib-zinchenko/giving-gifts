using System.ComponentModel.DataAnnotations;

namespace GivingGifts.Users.Infrastructure.JWT;

public class JwtOptions
{
    [Required] public string ValidAudience { get; set; }

    [Required] public string ValidIssuer { get; set; }

    [Required] public string Secret { get; set; }

    [Range(1, int.MaxValue)] public int TokenExpiresMinutes { get; set; }

    [Range(1, int.MaxValue)] public int RefreshTokenExpiresMinutes { get; set; }
}