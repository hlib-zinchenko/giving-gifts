using SharedKernel;

namespace GivingGifts.Wishlists.Core.Entities;

public class Wish : EntityBase<Guid>
{
    public string Name { get; private set; } = null!;
    public string? Url { get; private set; } = null!;
    public string? Notes { get; private set; } = null!;

    private Wish()
    {
    }

    public Wish(string name, string? url)
    {
        Name = name;
        Url = url;
    }

    public void UpdateWish(string name, string? url, string? notes)
    {
        Name = name;
        Url = url;
        Notes = notes;
    }
}