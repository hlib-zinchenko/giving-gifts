using MediatR;

namespace GivingGifts.SharedKernel.Core;

public abstract class DomainEventBase : INotification
{
    protected DomainEventBase(IDateTimeProvider dateTimeProvider)
    {
        DateTimeUtcOccured = dateTimeProvider.UtcNow;
    }

    public DateTime DateTimeUtcOccured { get; set; }
}