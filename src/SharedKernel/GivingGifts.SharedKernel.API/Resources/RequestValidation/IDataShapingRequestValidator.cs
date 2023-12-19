using System.Reflection;

namespace GivingGifts.SharedKernel.API.Resources.RequestValidation;

public interface IDataShapingRequestValidator
{
    DataShapingRequestValidationResult ValidateDataShaping<TResource>(IDataShapingRequest<TResource> request);
    string[] GetValidToRequestFields<TResource>();

    public bool ShouldApplyShaping<TResource>(
        IDataShapingRequest<TResource> request,
        out PropertyInfo[] shapeReadyFields);
}