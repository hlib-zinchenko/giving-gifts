using System.Reflection;

namespace GivingGifts.SharedKernel.API.Resources;

public static class DataShapingRequestExtensions
{
    public static bool ValidateRequest<TResource>(
        this IDataShapingRequest<TResource> request,
        out string[] invalidParameters)
    {
        var fields = request.GetDataShapingFields().ToArray();
        if (fields.Length == 0)
        {
            invalidParameters = Array.Empty<string>();
            return true;
        }

        var resourceTypeProperties = typeof(TResource)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.Name.ToLowerInvariant())
            .ToHashSet();

        invalidParameters = fields
            .Where(f => !resourceTypeProperties.Contains(f))
            .ToArray();
        return invalidParameters.Length == 0;
    }

    public static bool ShouldApplyShaping<TResource>(
        this IDataShapingRequest<TResource> request,
        out PropertyInfo[] shapeReadyFields)
    {
        var requestedFields = request.GetDataShapingFields().ToArray();
        if (requestedFields.Length == 0)
        {
            shapeReadyFields = Array.Empty<PropertyInfo>();
            return false;
        }
        var properties = typeof(TResource)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        shapeReadyFields = properties
            .Where(p =>
                requestedFields.Any(rf => rf
                    .Equals(p.Name, StringComparison.InvariantCultureIgnoreCase)))
            .ToArray();
        return true;
    }
}