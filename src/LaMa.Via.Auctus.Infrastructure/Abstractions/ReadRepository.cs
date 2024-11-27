using LaMa.Via.Auctus.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.Abstractions;


public abstract class ReadRepository<TEntity> : IReadRepository where TEntity : class
{
    private readonly DbContext _context;

    protected ReadRepository(DbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> Entity => _context.Set<TEntity>();
}