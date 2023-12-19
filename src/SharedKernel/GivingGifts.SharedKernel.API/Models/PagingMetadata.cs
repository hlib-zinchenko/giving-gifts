namespace GivingGifts.SharedKernel.API.Models;

public record PagingMetadata(
    int CurrentPage,
    int TotalCount,
    int PageSize,
    int TotalPages,
    string? PreviousPageLink = null,
    string? NextPageLink = null);