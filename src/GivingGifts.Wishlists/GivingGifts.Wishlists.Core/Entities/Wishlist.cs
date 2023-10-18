using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace GivingGifts.Wishlists.Core.Entities;

public class Wishlist : EntityBase<Guid>, IAggregationRoot
{
    public string Name { get; } = null!;
    public Guid UserId { get; }

    [NotMapped] public IReadOnlyCollection<Wish> Wishes => _wishes.AsReadOnly();

    private List<Wish> _wishes = new ();

    private Wishlist()
    {
    }

    public Wishlist(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }

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
}