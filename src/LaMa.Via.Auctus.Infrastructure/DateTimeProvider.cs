using LaMa.Via.Auctus.Application.Common;
using NodaTime;
using NodaTime.Extensions;

namespace LaMa.Via.Auctus.Infrastructure;

public class DateTimeProvider(IClock clock) : IDateTimeProvider
{
    public DateTimeOffset GetUtcNow()
    {
        return clock.InUtc().GetCurrentOffsetDateTime().ToDateTimeOffset();
    }

    public Instant GetCurrentInstant()
    {
        return clock.GetCurrentInstant();
    }

    public DateTimeOffset GetZonedNow(DateTimeZone zone)
    {
        return clock.InZone(zone).GetCurrentOffsetDateTime().ToDateTimeOffset();
    }
}