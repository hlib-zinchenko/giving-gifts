namespace SharedKernel;

public abstract class DomainEventBase
{
    protected DomainEventBase(IDateTimeProvider dateTimeProvider)
    {
        DateTimeUtcOccured = dateTimeProvider.UtcNow;
    }
    public DateTime DateTimeUtcOccured { get; set; }
}