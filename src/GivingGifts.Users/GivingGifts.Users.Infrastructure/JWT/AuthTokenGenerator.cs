using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Users.Core.Entities;
using GivingGifts.Users.UseCases;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GivingGifts.Users.Infrastructure.JWT;

public class AuthTokenGenerator : IAuthTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtOptions _options;

    public AuthTokenGenerator(
        IOptions<JwtOptions> options,
        IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _options = options.Value;
    }

    public AuthTokensDto Generate(User user)
    {
        var key = Encoding.UTF8.GetBytes
            (_options.Secret);

        var authClaims = new List<Claim>
        {
            new(ClaimNames.UserId, user.Id.ToString()),
            new(ClaimNames.Email, user.Email!),
            new(ClaimNames.FirstName, user.FirstName),
            new(ClaimNames.LastName, user.LastName),
            new(ClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimNames.Role, user.UserRoles.First().Role.Name!)
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(authClaims),
            Expires = _dateTimeProvider.UtcNow.AddMinutes(_options.TokenExpiresMinutes),
            Issuer = _options.ValidIssuer,
            Audience = _options.ValidAudience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        return new AuthTokensDto(stringToken);
    }
}