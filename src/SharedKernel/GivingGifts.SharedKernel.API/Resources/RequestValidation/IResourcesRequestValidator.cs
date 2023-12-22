using GivingGifts.SharedKernel.API.Resources.RequestValidation.Validators;

namespace GivingGifts.SharedKernel.API.Resources.RequestValidation;

public interface IResourcesRequestValidator
{
    ResourcesRequestValidationResult<TResource> Validate<TResource, TDto>(ResourcesRequestBase<TResource> request);
}