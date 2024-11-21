using LaMa.Via.Auctus.Domain.CarManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Write;

public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelType>
{
    public void Configure(EntityTypeBuilder<FuelType> builder)
    {
        builder.ToTable("FuelTypes");
        builder.HasKey(model => model.Id);
        builder.Property(model => model.Id)
            .HasConversion(model => model,
                value => FuelType.Create(value).Value.Id).IsRequired().HasMaxLength(255);

        builder.Property<uint>("Version").IsRowVersion();

        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<FuelType> builder)
    {
        builder.HasData(
            FuelType.Create("Gasoline").Value,
            FuelType.Create("Diesel").Value,
            FuelType.Create("Compressed Natural Gas (CNG)").Value,
            FuelType.Create("Liquefied Natural Gas (LNG)").Value,
            FuelType.Create("Liquefied Petroleum Gas (LPG)").Value,
            FuelType.Create("Ethanol").Value,
            FuelType.Create("Biodiesel").Value,
            FuelType.Create("Hydrogen Fuel Cell").Value,
            FuelType.Create("Electric").Value,
            FuelType.Create("Hybrid").Value,
            FuelType.Create("Plug-in Hybrid").Value,
            FuelType.Create("Propane").Value);
    }
}