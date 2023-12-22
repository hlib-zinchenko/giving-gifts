using Ardalis.GuardClauses;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.WishlistAggregate.DomainEvents;
using GivingGifts.Wishlists.Core.WishlistAggregate.Entities;

namespace GivingGifts.Wishlists.Core.WishlistAggregate;

public class Wishlist : EntityBase<Guid>, IAggregationRoot
{
    private readonly List<Wish> _wishes = [];

    // ReSharper disable once UnusedMember.Local
    private Wishlist()
    {
    }

    public Wishlist(Guid id, Guid userId, string name)
    {
        Guard.Against.Default(id, nameof(id));
        Guard.Against.Default(userId, nameof(userId));
        Guard.Against.NullOrEmpty(name, nameof(name));
        Id = id;
        UserId = userId;
        Name = name;
    }

    public string Name { get; private set; } = null!;
    public Guid UserId { get; }

    public IEnumerable<Wish> Wishes => _wishes.AsReadOnly();

    public void AddWish(Wish wish)
    {
        Guard.Against.Null(wish, nameof(wish));
        _wishes.Add(wish);
    }

    public void AddWishes(Wish[] wishes)
    {
        Guard.Against.Null(wishes, nameof(wishes));
        _wishes.AddRange(wishes);
    }

    public void RemoveWish(Wish wish)
    {
        _wishes.Remove(wish);
    }

    public void Update(
        string name,
        IDateTimeProvider dateTimeProvider)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        Name = name;

        _events.Add(new WishlistUpdatedEvent(dateTimeProvider, Id));
    }
}