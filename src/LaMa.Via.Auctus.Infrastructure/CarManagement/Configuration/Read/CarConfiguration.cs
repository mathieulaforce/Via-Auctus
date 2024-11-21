using LaMa.Via.Auctus.Application.CarManagement.Models;
using LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Read;

public class CarConfiguration : IEntityTypeConfiguration<CarDao>
{
    public void Configure(EntityTypeBuilder<CarDao> builder)
    {
        builder.ToTable("Cars");
        builder.HasKey(car => car.Id);

        builder.OwnsOne(car => car.Registration,
            registrationBuilder => { registrationBuilder.RegisterCarRegistrationValueObject(); });

        builder.HasOne<CarBrandDao>(car => car.Brand)
            .WithMany()
            .HasForeignKey(model => model.BrandId);

        builder.HasOne<CarModelDao>(car => car.Model)
            .WithMany()
            .HasForeignKey(model => model.ModelId);

        builder.HasOne<CarModelVersionDao>(car => car.Version)
            .WithMany()
            .HasForeignKey(model => model.VersionId);

        builder.HasOne<EngineDao>(car => car.Engine)
            .WithMany()
            .HasForeignKey(model => model.EngineId);
    }
}