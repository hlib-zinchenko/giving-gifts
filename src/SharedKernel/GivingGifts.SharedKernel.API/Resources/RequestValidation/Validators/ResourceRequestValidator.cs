namespace GivingGifts.SharedKernel.API.Resources.RequestValidation.Validators;

public class ResourceRequestValidator : IResourceRequestValidator
{
    private readonly IDataShapingRequestValidator _dataShapingRequestValidator;

    public ResourceRequestValidator(
        IDataShapingRequestValidator dataShapingRequestValidator)
    {
        _dataShapingRequestValidator = dataShapingRequestValidator;
    }

    public ResourceRequestValidationResult<TResource> Validate<TResource>(ResourceRequestBase<TResource> request)
    {
        var dataShapingRequestValidationResult = _dataShapingRequestValidator.ValidateDataShaping(request);

        return new ResourceRequestValidationResult<TResource>(dataShapingRequestValidationResult);
    }
}