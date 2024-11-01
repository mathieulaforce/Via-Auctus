namespace LaMa.Via.Auctus.Domain.Abstractions;

public class UniqueIdGenerator
{
    public static Guid Generate()
    {
        return Guid.NewGuid();
    }
}