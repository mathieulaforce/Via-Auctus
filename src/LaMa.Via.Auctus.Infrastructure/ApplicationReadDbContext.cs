using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure;

public class ApplicationReadDbContext : DbContext
{
    public ApplicationReadDbContext(
        DbContextOptions<ApplicationReadDbContext> options
    ) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.DbSchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationWriteDbContext).Assembly,
            type => type.FullName?.Contains(".Configuration.Read") ?? false);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.EnableSensitiveDataLogging();
#endif
        base.OnConfiguring(optionsBuilder);
    }

    public override int SaveChanges()
    {
        throw new Exception();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new Exception();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        throw new Exception();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        throw new Exception();
    }
}