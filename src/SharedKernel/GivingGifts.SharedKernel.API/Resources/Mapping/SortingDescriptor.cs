namespace GivingGifts.SharedKernel.API.Resources.Mapping;

public class SortingDescriptor
{
    public string RequestedPropertyName { get; }
    public List<SortablePropertyInfo> SortablePropertyInfos { get; }

    public SortingDescriptor(string requestedPropertyName, List<SortablePropertyInfo> sortablePropertyInfos)
    {
        RequestedPropertyName = requestedPropertyName;
        SortablePropertyInfos = sortablePropertyInfos;
    }
}