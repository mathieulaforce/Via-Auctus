using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Write;

public class EngineConfiguration : IEntityTypeConfiguration<Engine>
{
    public void Configure(EntityTypeBuilder<Engine> builder)
    {
        builder.ToTable("Engines");
        builder.HasKey(engine => engine.Id);

        builder.Property(engine => engine.Id)
            .HasConversion(engine => engine.Value,
                value => EngineId.Create(value)).IsRequired();

        builder.Property(engine => engine.Name).IsRequired().HasMaxLength(255);
        builder.Property(engine => engine.Torque);
        builder.Property(engine => engine.HorsePower);
        builder.HasOne(engine => engine.FuelType).WithMany();

        builder.OwnsOne(engine => engine.Efficiency,
            efficiencyBuilder => { efficiencyBuilder.RegisterEngineEfficiencyValueObject(); });

        builder.Property<uint>("Version").IsRowVersion();
    }
}