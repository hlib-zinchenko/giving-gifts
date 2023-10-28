using GivingGifts.SharedKernel.Core;

namespace GivingGifts.Wishlists.Core.Entities;

public class Wishlist : EntityBase<Guid>, IAggregationRoot
{
    private readonly List<Wish> _wishes = new();

    private Wishlist()
    {
    }

    public Wishlist(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }

    public string Name { get; private set; } = null!;
    public Guid UserId { get; }

    public IEnumerable<Wish> Wishes => _wishes.AsReadOnly();

    public Wish AddWish(string name, string? url)
    {
        var wish = new Wish(name, url);
        _wishes.Add(wish);
        return wish;
    }

    public void RemoveWish(Wish wish)
    {
        _wishes.Remove(wish);
    }

    public void Update(string name)
    {
        Name = name;
    }
}