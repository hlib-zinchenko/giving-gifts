namespace GivingGifts.SharedKernel.API.Resources.RequestValidation;

public interface IResourcesRequestValidator
{
    ResourcesRequestValidationResult<TResource> Validate<TResource, TDto>(ResourcesRequestBase<TResource> request);
}

public interface IResourceRequestValidator
{
    ResourceRequestValidationResult<TResource> Validate<TResource>(ResourceRequestBase<TResource> request);
}

public record ResourceValidRequestParameters(string[] Fields);

public record ResourcesValidRequestParameters(string[] Fields, string[] SortBy);

public class ResourcesRequestValidationResult<TResource>(
    DataShapingRequestValidationResult dataShapingRequestValidationResult,
    SortingRequestValidationResult sortingRequestValidationResult)
{
    public DataShapingRequestValidationResult DataShapingRequestValidationResult
        => dataShapingRequestValidationResult;
    public SortingRequestValidationResult SortingRequestValidationResult
        => sortingRequestValidationResult;
    public bool IsValid => dataShapingRequestValidationResult.IsValid
                           && sortingRequestValidationResult.IsValid;

    public Dictionary<string, string> GetValidationErrors()
    {
        var result = new Dictionary<string, string>();
        if (!dataShapingRequestValidationResult.IsValid)
        {
            result[nameof(IDataShapingRequest.Fields)] =
                $"Field(s) '{string.Join(", ", dataShapingRequestValidationResult.InvalidProperties)}' " +
                $"does not exist in the resource '{typeof(TResource).Name}' or not data shapable";
        }

        if (!sortingRequestValidationResult.IsValid)
        {
            result[nameof(ISortingRequest.OrderBy)] =
                $"Ordering for field(s) '{string.Join(", ", sortingRequestValidationResult.InvalidProperties)}' " +
                $"is not possible";
        }

        return result;
    }
}

public class ResourceRequestValidationResult<TResource>(
    DataShapingRequestValidationResult dataShapingRequestValidationResult)
{
    public DataShapingRequestValidationResult DataShapingRequestValidationResult
        => dataShapingRequestValidationResult;
    public bool IsValid => dataShapingRequestValidationResult.IsValid;

    public Dictionary<string, string> GetValidationErrors()
    {
        var result = new Dictionary<string, string>();
        if (!dataShapingRequestValidationResult.IsValid)
        {
            result[nameof(IDataShapingRequest.Fields)] =
                $"Field(s) '{string.Join(", ", dataShapingRequestValidationResult.InvalidProperties)}' " +
                $"does not exist in the resource '{typeof(TResource).Name}' or not data shapable";
        }

        return result;
    }
}

public record DataShapingRequestValidationResult(
    bool IsValid,
    string[] ValidToRequestProperties,
    string[] InvalidProperties);

public record SortingRequestValidationResult(
    bool IsValid,
    string[] ValidToRequestProperties,
    string[] InvalidProperties);