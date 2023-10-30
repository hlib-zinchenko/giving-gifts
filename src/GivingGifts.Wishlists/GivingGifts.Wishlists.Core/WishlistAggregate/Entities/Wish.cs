using Ardalis.GuardClauses;
using GivingGifts.SharedKernel.Core;

namespace GivingGifts.Wishlists.Core.WishlistAggregate.Entities;

public class Wish : EntityBase<Guid>
{
    private Wish()
    {
    }

    public Wish(Guid id, string name, string? url)
    {
        Guard.Against.Default(id, nameof(id));
        Guard.Against.NullOrEmpty(name, nameof(name));
        Id = id;
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