using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure;

public partial class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly DomainEventInterceptor _domainEventInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        DomainEventInterceptor domainEventInterceptor
    ) : base(options)
    {
        _domainEventInterceptor = domainEventInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif

        optionsBuilder
            .AddInterceptors(_domainEventInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}