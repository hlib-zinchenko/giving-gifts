using GivingGifts.SharedKernel.Core;

namespace GivingGifts.Wishlists.Core.Entities;

public class Wish : EntityBase<Guid>
{
    private Wish()
    {
    }

    public Wish(string name, string? url)
    {
        Name = name;
        Url = url;
    }

    public string Name { get; private set; } = null!;
    public string? Url { get; private set; }
    public string? Notes { get; private set; }

    public void UpdateWish(string name, string? url, string? notes)
    {
        Name = name;
        Url = url;
        Notes = notes;
    }
}