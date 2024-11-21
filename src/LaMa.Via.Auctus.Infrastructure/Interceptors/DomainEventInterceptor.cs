using LaMa.Via.Auctus.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LaMa.Via.Auctus.Infrastructure.Interceptors;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public DomainEventInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context is not null)
        {
            PublishDomainEvents(context).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context is not null)
        {
            await PublishDomainEvents(context);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext context)
    {
        var domainEvents = context.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}