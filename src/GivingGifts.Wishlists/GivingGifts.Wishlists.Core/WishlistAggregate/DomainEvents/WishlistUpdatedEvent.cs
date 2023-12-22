using GivingGifts.SharedKernel.Core;

namespace GivingGifts.Wishlists.Core.WishlistAggregate.DomainEvents;

public class WishlistUpdatedEvent(
    IDateTimeProvider dateTimeProvider,
    Guid wishlistId)
    : DomainEventBase(dateTimeProvider)
{
    public Guid WishlistId { get; } = wishlistId;
}