using GivingGifts.Users.Core.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GivingGifts.Users.Core.EventHandlers;

// ReSharper disable once UnusedType.Global
public class UserCreatedEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;

    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handled {Event} for {UserId} occured at {Occured}",
            nameof(UserCreatedEventHandler),
            notification.UserId,
            notification.DateTimeUtcOccured);
        return Task.CompletedTask;
    }
}