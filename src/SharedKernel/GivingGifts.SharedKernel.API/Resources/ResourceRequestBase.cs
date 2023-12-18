using GivingGifts.SharedKernel.API.Resources.Utils;

namespace GivingGifts.SharedKernel.API.Resources;

public abstract class ResourceRequestBase<TResource> : IDataShapingRequest<TResource>
{
    public string? Fields { get; set; } = string.Empty;
    private readonly Lazy<IEnumerable<string>> _dataShapingFields;

    protected ResourceRequestBase()
    {
        _dataShapingFields = new Lazy<IEnumerable<string>>(() => StringsParser.ParseDataShapingString(Fields));
    }
    public IEnumerable<string> GetDataShapingFields()
    {
        return _dataShapingFields.Value;
    }
}