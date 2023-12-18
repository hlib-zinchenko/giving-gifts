namespace GivingGifts.SharedKernel.API.Resources;

// ReSharper disable once UnusedTypeParameter
public interface IDataShapingRequest<TResource> : IDataShapingRequest;

public interface IDataShapingRequest
{
    string? Fields { get; set; }

    IEnumerable<string> GetDataShapingFields();
}