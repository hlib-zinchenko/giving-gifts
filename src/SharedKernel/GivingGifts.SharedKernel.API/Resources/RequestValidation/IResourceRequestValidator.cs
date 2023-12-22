using GivingGifts.SharedKernel.API.Resources.RequestValidation.Validators;

namespace GivingGifts.SharedKernel.API.Resources.RequestValidation;

public interface IResourceRequestValidator
{
    ResourceRequestValidationResult<TResource> Validate<TResource>(ResourceRequestBase<TResource> request);
}