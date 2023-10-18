using Ardalis.Result;
using Microsoft.AspNetCore.Identity;

namespace GivingGifts.Users.Core.Extensions;

public static class IdentityResultExtensions
{
    public static IEnumerable<ValidationError> AsErrors(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
        {
            return Array.Empty<ValidationError>();
        }
        
        return identityResult.Errors.Select(e => new ValidationError
        {
            Identifier = string.Empty,
            ErrorMessage = e.Description,
            ErrorCode = e.Code,
            Severity = ValidationSeverity.Error,
        });
    }
}