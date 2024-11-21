using LaMa.Via.Auctus.Application.CarManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Read;

public class CarBrandConfiguration : IEntityTypeConfiguration<CarBrandDao>
{
    public void Configure(EntityTypeBuilder<CarBrandDao> builder)
    {
        builder.ToTable("CarBrands");
        builder.HasKey(brand => brand.Id);

        builder.HasMany(brand => brand.Models)
            .WithOne(model => model.Brand)
            .HasForeignKey(model => model.BrandId);
    }
}