namespace GivingGifts.SharedKernel.API.Resources.RequestValidation.Validators;

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