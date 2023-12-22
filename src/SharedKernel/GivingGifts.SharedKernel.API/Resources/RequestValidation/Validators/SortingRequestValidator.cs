using GivingGifts.SharedKernel.API.Resources.Mapping;

namespace GivingGifts.SharedKernel.API.Resources.RequestValidation.Validators;

public class SortingRequestValidator : ISortingRequestValidator
{
    private readonly IResourceMapper _resourceMapper;

    public SortingRequestValidator(IResourceMapper resourceMapper)
    {
        _resourceMapper = resourceMapper;
    }

    public SortingRequestValidationResult ValidateSorting<TResource, TDto>(ISortingRequest<TResource> request)
    {
        var configuredSortings = _resourceMapper.GetConfiguredSortableFields<TDto, TResource>();
        var sortingEntries = request.GetSortingEntries().ToArray();
        if (sortingEntries.Length == 0)
        {
            return new SortingRequestValidationResult(
                true,
                configuredSortings,
                Array.Empty<string>());
        }

        var invalidEntries = sortingEntries
            .Where(sp =>
                !configuredSortings.Contains(sp.SortBy)).ToArray();
        return new SortingRequestValidationResult(
            invalidEntries.Length == 0,
            configuredSortings,
            invalidEntries.Select(e => e.SortBy).ToArray());
    }

    public string[] GetValidToRequestFields<TResource, TDto>()
    {
        return _resourceMapper.GetConfiguredSortableFields<TDto, TResource>();
    }
}