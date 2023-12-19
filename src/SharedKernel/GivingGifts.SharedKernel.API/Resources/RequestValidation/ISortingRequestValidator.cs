namespace GivingGifts.SharedKernel.API.Resources.RequestValidation;

public interface ISortingRequestValidator
{
    SortingRequestValidationResult ValidateSorting<TResource, TDto>(ISortingRequest<TResource> request);
    string[] GetValidToRequestFields<TResource, TDto>(IDataShapingRequest<TResource> request);
}