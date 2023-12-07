using FluentValidation;

namespace GivingGifts.SharedKernel.Core.Extensions;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string?> ValidUrl<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(ValidUrl)
            .WithMessage("{PropertyName} is not a valid URL.");
    }

    private static bool ValidUrl(string? url)
    {
        if (url == null)
        {
            return true;
        }
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}