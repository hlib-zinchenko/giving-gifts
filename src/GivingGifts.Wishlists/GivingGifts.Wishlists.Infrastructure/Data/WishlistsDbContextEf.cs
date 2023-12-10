using System.Reflection;
using GivingGifts.SharedKernel.Core;
using GivingGifts.Wishlists.Core.WishlistAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GivingGifts.Wishlists.Infrastructure.Data;

public class WishlistsDbContextEf : DbContext
{
    private readonly IMediator _mediator;

    public WishlistsDbContextEf(
        DbContextOptions<WishlistsDbContextEf> dbContextOptions,
        IMediator mediator)
        : base(dbContextOptions)
    {
        _mediator = mediator;
    }

    public DbSet<Wishlist> Wishlists { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        await SendEvents();
        return result;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        await SendEvents();
        return result;
    }

    private async Task SendEvents()
    {
        // ignore events if no dispatcher provided
        if (_mediator == null)
        {
            return;
        }

        var entitiesWithEvents = ChangeTracker
            .Entries()
            .Select(e => e.Entity as IEntity)
            .Where(e => e is { Events: not null } && e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity!.Events.ToArray();
            entity.ClearEvents();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
}