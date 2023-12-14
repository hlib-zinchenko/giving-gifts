namespace GivingGifts.SharedKernel.API.Models;

public abstract class ResourceRequest<TResource> : IDataShapingRequest<TResource>
{
    public string? Fields { get; set; } = string.Empty;
}