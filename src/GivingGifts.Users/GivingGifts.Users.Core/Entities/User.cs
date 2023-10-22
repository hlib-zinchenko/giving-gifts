using System.ComponentModel.DataAnnotations.Schema;
using GivingGifts.Users.Core.DomainEvents;
using Microsoft.AspNetCore.Identity;
using GivingGifts.SharedKernel.Core;

namespace GivingGifts.Users.Core.Entities;

public sealed class User : IdentityUser<Guid>, IEntity<Guid>, IAggregationRoot
{
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
        UserName = email;
        _events.Add(new UserCreatedDomainEvent(dateTimeProvider, Id));
    }

    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public List<UserRole> UserRoles { get; } = new();
    [NotMapped] public IReadOnlyCollection<DomainEventBase> Events => _events.AsReadOnly();
}