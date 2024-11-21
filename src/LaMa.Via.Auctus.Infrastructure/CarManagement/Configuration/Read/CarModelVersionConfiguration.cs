using LaMa.Via.Auctus.Application.CarManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Read;

public class CarModelVersionConfiguration : IEntityTypeConfiguration<CarModelVersionDao>
{
    public void Configure(EntityTypeBuilder<CarModelVersionDao> builder)
    {
        builder.ToTable("CarModelVersions");
        builder.HasKey(version => version.Id);

        builder.HasMany(version => version.Engines)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ModelVersionEngines", // Join table name
                entityBuilder => entityBuilder.HasOne<EngineDao>().WithMany().HasForeignKey("EngineId"),
                entityBuilder => entityBuilder.HasOne<CarModelVersionDao>().WithMany().HasForeignKey("ModelVersionId"));
        ;

        builder.HasOne<CarModelDao>(car => car.CarModel)
            .WithMany(brand => brand.Versions)
            .HasForeignKey(model => model.CarModelId);
    }
}