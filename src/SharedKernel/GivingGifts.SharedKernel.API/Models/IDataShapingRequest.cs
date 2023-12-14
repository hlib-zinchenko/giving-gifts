namespace GivingGifts.SharedKernel.API.Models;

public interface IDataShapingRequest<TResource> : IDataShapingRequest
{
}

public interface IDataShapingRequest
{
    string? Fields { get; set; }

    IEnumerable<string> GetFields() => string.IsNullOrWhiteSpace(Fields)
        ? Array.Empty<string>()
        : Fields.Trim().Split(",").Select(s => s.Trim().ToLowerInvariant());
}