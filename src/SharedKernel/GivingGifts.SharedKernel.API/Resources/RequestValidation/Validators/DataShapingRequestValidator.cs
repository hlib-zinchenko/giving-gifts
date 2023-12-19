using System.Reflection;

namespace GivingGifts.SharedKernel.API.Resources.RequestValidation.Validators;

public class DataShapingRequestValidator : IDataShapingRequestValidator
{
    public DataShapingRequestValidationResult ValidateDataShaping<TResource>(IDataShapingRequest<TResource> request)
    {
        var resourceTypeProperties = GetShapableProperties<TResource>()
            .Select(p => p.Name.ToLowerInvariant())
            .ToHashSet();

        var fields = request.GetDataShapingFields().ToArray();
        if (fields.Length == 0)
        {
            return new DataShapingRequestValidationResult(
                true,
                resourceTypeProperties.ToArray(),
                Array.Empty<string>());
        }

        var invalidParameters = fields
            .Where(f => !resourceTypeProperties.Contains(f))
            .ToArray();
        return new DataShapingRequestValidationResult(
            invalidParameters.Length == 0,
            resourceTypeProperties.ToArray(),
            invalidParameters);
    }

    public string[] GetValidToRequestFields<TResource>()
    {
        return GetShapableProperties<TResource>()
            .Select(p => p.Name.ToLowerInvariant())
            .ToArray();
    }
    
    public bool ShouldApplyShaping<TResource>(
        IDataShapingRequest<TResource> request,
        out PropertyInfo[] shapeReadyFields)
    {
        var requestedFields = request.GetDataShapingFields().ToArray();
        if (requestedFields.Length == 0)
        {
            shapeReadyFields = Array.Empty<PropertyInfo>();
            return false;
        }
        var properties = GetShapableProperties<TResource>();

        shapeReadyFields = properties
            .Where(p =>
                requestedFields.Any(rf => rf
                    .Equals(p.Name, StringComparison.InvariantCultureIgnoreCase)))
            .ToArray();
        return true;
    }

    private static PropertyInfo[] GetShapableProperties<TResource>()
    {
        var properties = typeof(TResource)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetCustomAttribute<NotDataShapableAttribute>() == null)
            .ToArray();
        return properties;
    }
}