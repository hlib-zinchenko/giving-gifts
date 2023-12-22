namespace GivingGifts.SharedKernel.API.Resources.RequestValidation;

public record SortingRequestValidationResult(
    bool IsValid,
    string[] ValidToRequestProperties,
    string[] InvalidProperties);