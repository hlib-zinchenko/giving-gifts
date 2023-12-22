namespace GivingGifts.SharedKernel.API.Resources.RequestValidation;

public record DataShapingRequestValidationResult(
    bool IsValid,
    string[] ValidToRequestProperties,
    string[] InvalidProperties);