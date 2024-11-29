using NodaTime;

namespace LaMa.Via.Auctus.Application.Common;

public interface IDateTimeProvider
{
    public DateTimeOffset GetUtcNow();
    public Instant GetCurrentInstant();
    public DateTimeOffset GetZonedNow(DateTimeZone zone);
}