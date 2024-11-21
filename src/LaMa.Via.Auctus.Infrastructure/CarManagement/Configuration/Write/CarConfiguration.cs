using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Write;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars");
        builder.HasKey(car => car.Id);

        builder.Property(car => car.Id)
            .HasConversion(carId => carId.Value,
                value => CarId.Create(value)).IsRequired();

        builder.OwnsOne(car => car.Registration,
            registrationBuilder => { registrationBuilder.RegisterCarRegistrationValueObject(); });


        builder.HasOne<CarBrand>()
            .WithMany()
            .HasForeignKey(model => model.BrandId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne<CarModel>()
            .WithMany()
            .HasForeignKey(model => model.ModelId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne<CarModelVersion>()
            .WithMany()
            .HasForeignKey(model => model.VersionId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne<Engine>()
            .WithMany()
            .HasForeignKey(model => model.EngineId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Property<uint>("Version").IsRowVersion();
    }
}