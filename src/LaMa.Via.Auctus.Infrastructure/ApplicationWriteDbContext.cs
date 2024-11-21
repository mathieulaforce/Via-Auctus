using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure;

public class ApplicationWriteDbContext : DbContext, IUnitOfWork
{
    private readonly DomainEventInterceptor _domainEventInterceptor;

    public ApplicationWriteDbContext(
        DbContextOptions<ApplicationWriteDbContext> options,
        DomainEventInterceptor domainEventInterceptor
    ) : base(options)
    {
        _domainEventInterceptor = domainEventInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.DbSchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationWriteDbContext).Assembly,
            type => type.FullName?.Contains(".Configuration.Write") ?? false);
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