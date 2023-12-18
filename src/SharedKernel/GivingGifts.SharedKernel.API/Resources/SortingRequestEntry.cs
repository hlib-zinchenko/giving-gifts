using System.ComponentModel;

namespace GivingGifts.SharedKernel.API.Resources;

public record SortingRequestEntry(string SortBy, ListSortDirection SortDirection);