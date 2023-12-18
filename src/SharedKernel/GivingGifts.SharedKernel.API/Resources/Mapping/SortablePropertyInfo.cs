using System.ComponentModel;
using GivingGifts.SharedKernel.Core;

namespace GivingGifts.SharedKernel.API.Resources.Mapping;

public class SortablePropertyInfo(string sourceProp, bool reverseSorting)
{
    private string SourceProp { get; } = sourceProp;
    private bool ReverseSorting { get; } = reverseSorting;

    public SortingParameter ToSortingParams(ListSortDirection listSortDirection)
    {
        if (ReverseSorting)
        {
            return new SortingParameter(SourceProp, listSortDirection == ListSortDirection.Ascending
                ? ListSortDirection.Descending
                : ListSortDirection.Ascending);
        }

        return new SortingParameter(SourceProp, listSortDirection);
    }
}