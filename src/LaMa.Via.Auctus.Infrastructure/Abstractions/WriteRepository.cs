using LaMa.Via.Auctus.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.Abstractions;

public interface IWriteRepository<TEntity, TEntityId> : IWriteRepository where TEntity : class
{
    Task Add(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> Get(TEntityId id, CancellationToken cancellationToken = default);
}

internal abstract class WriteRepository<TEntity, TEntityId> : IWriteRepository<TEntity, TEntityId> where TEntity : class
{
    private readonly DbContext _context;

    protected WriteRepository(DbContext context)
    {
        _context = context;
    }

    protected DbSet<TEntity> Entity => _context.Set<TEntity>();

    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entity.AddAsync(entity, cancellationToken);
    }

    public virtual async Task<TEntity?> Get(TEntityId id, CancellationToken cancellationToken = default)
    {
        return await Entity.FindAsync(id, cancellationToken);
    }
}