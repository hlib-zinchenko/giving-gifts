using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GivingGifts.WebAPI.Auth;

/// <summary>
///     Available Claim Names.
/// </summary>
public static class ClaimNames
{
    public const string UserId = "UserId";
    public const string Email = ClaimTypes.Email;
    public const string FirstName = "FirstName";
    public const string LastName = "LastName";
    public const string Jti = JwtRegisteredClaimNames.Jti;
    public const string Role = ClaimTypes.Role;
}