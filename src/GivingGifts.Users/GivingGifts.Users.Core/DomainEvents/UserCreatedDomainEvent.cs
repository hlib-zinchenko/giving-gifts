using SharedKernel;

namespace GivingGifts.Users.Core.DomainEvents;

public class UserCreatedDomainEvent : DomainEventBase
{
    public Guid UserId { get; }

    public UserCreatedDomainEvent(
        IDateTimeProvider dateTimeProvider,
        Guid userId)
        : base (dateTimeProvider)
    {
        UserId = userId;
    }
}