using System.ComponentModel.DataAnnotations.Schema;

namespace GivingGifts.SharedKernel.Core;

public abstract class EntityBase<T> : IEntity<T>
{
    protected List<DomainEventBase> _events { get; } = [];
    public T Id { get; protected set; } = default!;

    [NotMapped] public IReadOnlyCollection<DomainEventBase> Events => _events.AsReadOnly();
    public void ClearEvents()
    {
        _events.Clear();
    }
}