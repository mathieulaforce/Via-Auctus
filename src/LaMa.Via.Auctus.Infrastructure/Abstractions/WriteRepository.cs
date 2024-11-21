using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.Abstractions;

public interface IWriteRepository
{
}

public abstract class WriteRepository<TEntity> : IWriteRepository where TEntity : class
{
    private readonly DbContext _context;

    protected WriteRepository(DbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Entity => _context.Set<TEntity>();

    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entity.AddAsync(entity, cancellationToken);
    }
}