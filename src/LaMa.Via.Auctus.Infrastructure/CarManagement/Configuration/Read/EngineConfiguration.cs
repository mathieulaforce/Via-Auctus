using LaMa.Via.Auctus.Application.CarManagement.Models;
using LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Read;

public class EngineConfiguration : IEntityTypeConfiguration<EngineDao>
{
    public void Configure(EntityTypeBuilder<EngineDao> builder)
    {
        builder.ToTable("Engines");
        builder.HasKey(engine => engine.Id);
        builder.Property(engine => engine.FuelType).HasColumnName("FuelTypeId");

        builder.OwnsOne(engine => engine.Efficiency,
            efficiencyBuilder => { efficiencyBuilder.RegisterEngineEfficiencyValueObject(); });


        builder.Property<uint>("Version").IsRowVersion();
    }
}