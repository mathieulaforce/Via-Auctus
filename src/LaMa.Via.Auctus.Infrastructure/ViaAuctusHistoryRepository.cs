using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;

namespace LaMa.Via.Auctus.Infrastructure;

public class ViaAuctusHistoryRepository : NpgsqlHistoryRepository
{
    public ViaAuctusHistoryRepository(HistoryRepositoryDependencies dependencies) : base(dependencies)
    {
    }

    protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
    {
        base.ConfigureTable(history);
        history.Property<DateTime>("via_auctus_appliedOn").HasDefaultValue(DateTime.UtcNow);
    }
}