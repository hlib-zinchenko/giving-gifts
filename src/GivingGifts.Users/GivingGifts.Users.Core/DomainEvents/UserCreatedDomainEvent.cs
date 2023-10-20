using GivingGifts.SharedKernel.Core;

namespace GivingGifts.Users.Core.DomainEvents;

public class UserCreatedDomainEvent : DomainEventBase
{
    public UserCreatedDomainEvent(
        IDateTimeProvider dateTimeProvider,
        Guid userId)
        : base(dateTimeProvider)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}