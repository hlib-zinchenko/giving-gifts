namespace GivingGifts.SharedKernel.Core;

public interface IEntity<T>
{
    T Id { get; }
    IReadOnlyCollection<DomainEventBase> Events { get; }
}