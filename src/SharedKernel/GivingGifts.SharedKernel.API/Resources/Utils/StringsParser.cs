using System.ComponentModel;

namespace GivingGifts.SharedKernel.API.Resources.Utils;

internal static class StringsParser
{
    public static IEnumerable<string> ParseDataShapingString(string? input) => string.IsNullOrWhiteSpace(input)
        ? Array.Empty<string>()
        : input
            .Trim()
            .Split(",")
            .Select(s => s.Trim().ToLowerInvariant())
            .Distinct();

    public static IEnumerable<SortingRequestEntry> ParseSortByString(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Array.Empty<SortingRequestEntry>();
        }

        return input
            .Trim()
            .Split(",")
            .Select(p => p.Trim().ToLowerInvariant())
            .Select(p =>
            {
                if (p.EndsWith(" desc"))
                {
                    return new SortingRequestEntry(
                        p[..^5],
                        ListSortDirection.Descending);
                }

                return new SortingRequestEntry(p, ListSortDirection.Ascending);
            }).ToArray();
    }
}