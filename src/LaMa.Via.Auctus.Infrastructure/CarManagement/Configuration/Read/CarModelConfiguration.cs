using LaMa.Via.Auctus.Application.CarManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Read;

public class CarModelConfiguration : IEntityTypeConfiguration<CarModelDao>
{
    public void Configure(EntityTypeBuilder<CarModelDao> builder)
    {
        builder.ToTable("CarModels");
        builder.HasKey(model => model.Id);
        builder.Property(model => model.BrandId).HasColumnName("CarBrandId");

        builder.HasMany(model => model.Versions)
            .WithOne(version => version.CarModel)
            .HasForeignKey(model => model.CarModelId);

        builder.HasOne<CarBrandDao>(car => car.Brand)
            .WithMany(brand => brand.Models)
            .HasForeignKey(model => model.BrandId);

        builder.Property<uint>("Version").IsRowVersion();
    }
}