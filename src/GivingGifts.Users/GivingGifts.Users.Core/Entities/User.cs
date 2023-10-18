using System.ComponentModel.DataAnnotations.Schema;
using GivingGifts.Users.Core.DomainEvents;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace GivingGifts.Users.Core.Entities;

public sealed class User : IdentityUser<Guid>, IEntity<Guid>, IAggregationRoot
{
    public string FirstName { get; }
    public string LastName { get; }
    [NotMapped] public IReadOnlyCollection<DomainEventBase> Events => _events.AsReadOnly();

    private readonly List<DomainEventBase> _events = new();

    private User()
    {
    }

    public User(IDateTimeProvider dateTimeProvider, string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _events.Add(new UserCreatedDomainEvent(dateTimeProvider, Id));
    }
}