using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Write;

public class CarModelConfiguration : IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder.ToTable("CarModels");
        builder.HasKey(model => model.Id);

        builder.Property(model => model.Name).HasMaxLength(50).IsRequired().IsRequired().HasMaxLength(255);
        builder.Property(model => model.Id)
            .IsRequired()
            .HasConversion(model => model.Value,
                value => CarModelId.Create(value));

        builder.OwnsOne(model => model.Image, imgBuilder =>
        {
            RelationalPropertyBuilderExtensions.HasColumnName(imgBuilder.Property(img => img.Url), "ImageUrl")
                .HasConversion(
                    url => url,
                    url => SupportedImage.Create(url).Value.Url);
        });

        builder.HasOne<CarBrand>()
            .WithMany()
            .HasForeignKey(model => model.CarBrandId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Property<uint>("Version").IsRowVersion();
    }
}