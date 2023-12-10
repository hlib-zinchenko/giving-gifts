namespace GivingGifts.SharedKernel.Core;

public interface IEntity<T> : IEntity
{
    T Id { get; }
}

public interface IEntity
{
    IReadOnlyCollection<DomainEventBase> Events { get; }
    void ClearEvents();
}