using GivingGifts.Wishlists.Core.WishlistAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GivingGifts.Wishlists.Core.WishlistAggregate.Handlers;

public class WishlistUpdatedEventHandler : INotificationHandler<WishlistUpdatedEvent>
{
    private readonly ILogger<WishlistUpdatedEventHandler> _logger;

    public WishlistUpdatedEventHandler(ILogger<WishlistUpdatedEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(WishlistUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handled {Event} for {WishlistId} occured at {Occured}",
            nameof(WishlistUpdatedEventHandler),
            notification.WishlistId,
            notification.DateTimeUtcOccured);
        return Task.CompletedTask;
    }
}