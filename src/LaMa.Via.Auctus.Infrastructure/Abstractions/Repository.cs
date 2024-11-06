using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.Abstractions;

public abstract class Repository
{
}

public abstract class Repository<TEntity> : Repository where TEntity : class
{
    private readonly DbContext _context;
    public DbSet<TEntity> Entity => _context.Set<TEntity>();

    protected Repository(DbContext context)
    {
        _context = context;
    }

    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entity.AddAsync(entity, cancellationToken);
    }
}