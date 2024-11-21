using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Write;

public class CarBrandConfiguration : IEntityTypeConfiguration<CarBrand>
{
    public void Configure(EntityTypeBuilder<CarBrand> builder)
    {
        builder.ToTable("CarBrands");
        builder.HasKey(brand => brand.Id);
        builder.Property(brand => brand.Name).IsRequired().HasMaxLength(255);

        builder.Property(brand => brand.Id)
            .HasConversion(brand => brand.Value,
                value => CarBrandId.Create(value)).IsRequired();

        builder.OwnsOne(brand => brand.Theme, themeBuilder =>
        {
            themeBuilder.OwnsOne(theme => theme.PrimaryColor, colorBuilder =>
            {
                colorBuilder.Property(color => color.Code)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("Theme_PrimaryColor")
                    .HasConversion(
                        colorCode => colorCode,
                        code => CssColor.Create(code).Value.Code);
            });

            themeBuilder.OwnsOne(theme => theme.SecondaryColor, colorBuilder =>
            {
                colorBuilder.Property(color => color.Code)
                    .HasColumnName("Theme_SecondaryColor")
                    .HasMaxLength(30)
                    .HasConversion(
                        colorCode => colorCode,
                        code => code == null ? null : CssColor.Create(code).Value.Code);
            });

            themeBuilder.OwnsOne(theme => theme.Logo, logoBuilder =>
            {
                logoBuilder.Property(logo => logo.Url)
                    .IsRequired()
                    .HasColumnName("Theme_LogoUrl")
                    .HasConversion(
                        url => url,
                        url => SvgImage.Create(url).Value.Url);
            });
        });

        builder.Property<uint>("Version").IsRowVersion();
    }
}