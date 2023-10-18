using System.ComponentModel.DataAnnotations.Schema;

namespace SharedKernel;

public abstract class EntityBase<T> : IEntity<T>
{
    public T Id { get; protected set; } = default!;

    [NotMapped]
    public IReadOnlyCollection<DomainEventBase> Events => _events.AsReadOnly();

    protected List<DomainEventBase> _events { get; } = new();
}