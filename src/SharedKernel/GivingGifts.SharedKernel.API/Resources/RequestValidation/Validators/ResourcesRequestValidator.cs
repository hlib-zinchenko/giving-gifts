namespace GivingGifts.SharedKernel.API.Resources.RequestValidation.Validators;

public class ResourcesRequestValidator : IResourcesRequestValidator
{
    private readonly IDataShapingRequestValidator _dataShapingRequestValidator;
    private readonly ISortingRequestValidator _sortingRequestValidator;

    public ResourcesRequestValidator(
        IDataShapingRequestValidator dataShapingRequestValidator,
        ISortingRequestValidator sortingRequestValidator)
    {
        _dataShapingRequestValidator = dataShapingRequestValidator;
        _sortingRequestValidator = sortingRequestValidator;
    }

    public ResourcesRequestValidationResult<TResource> Validate<TResource, TDto>(
        ResourcesRequestBase<TResource> request)
    {
        var dataShapingRequestValidationResult = _dataShapingRequestValidator.ValidateDataShaping(request);
        var sortingRequestValidationResult = _sortingRequestValidator.ValidateSorting<TResource, TDto>(request);

        return new ResourcesRequestValidationResult<TResource>(dataShapingRequestValidationResult,
            sortingRequestValidationResult);
    }
}