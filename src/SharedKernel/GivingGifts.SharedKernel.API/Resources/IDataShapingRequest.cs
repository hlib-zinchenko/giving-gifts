namespace GivingGifts.SharedKernel.API.Resources;

public interface IDataShapingRequest<TResource> : IDataShapingRequest;

public interface IDataShapingRequest
{
    string? Fields { get; set; }

    IEnumerable<string> GetDataShapingFields();
}