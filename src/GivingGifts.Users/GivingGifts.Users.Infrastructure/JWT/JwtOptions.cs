using System.ComponentModel.DataAnnotations;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace GivingGifts.Users.Infrastructure.JWT;

public class JwtOptions
{
    [Required] public string ValidAudience { get; init; } = null!;

    [Required] public string ValidIssuer { get; init; } = null!;

    [Required] public string Secret { get; init; } = null!;

    [Range(1, int.MaxValue)] public int TokenExpiresMinutes { get; init; }

    [Range(1, int.MaxValue)] public int RefreshTokenExpiresMinutes { get; init; }
}