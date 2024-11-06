using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration;

public class CarModelVersionConfiguration : IEntityTypeConfiguration<CarModelVersion>
{
    public void Configure(EntityTypeBuilder<CarModelVersion> builder)
    {
        builder.ToTable("CarModelVersions");
        builder.HasKey(model => model.Id);

        builder.Property(model => model.Id)
            .IsRequired()
            .HasConversion(model => model.Value,
                value => CarModelVersionId.Create(value));

        builder.Property(model => model.Name)
            .IsRequired().HasMaxLength(255);

        builder.Property(model => model.Year)
            .IsRequired();


        builder.OwnsOne(model => model.Image, imgBuilder =>
        {
            imgBuilder.Property(img => img.Url)
                .HasColumnName("ImageUrl")
                .HasConversion(
                    url => url,
                    url => SupportedImage.Create(url).Value.Url);
        });

        builder.HasMany(modelVersion => modelVersion.Engines)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ModelVersionEngines", // Join table name
                entityBuilder => entityBuilder.HasOne<Engine>().WithMany().HasForeignKey("EngineId"),
                entityBuilder => entityBuilder.HasOne<CarModelVersion>().WithMany().HasForeignKey("ModelVersionId"));

        builder.HasOne<CarModel>()
            .WithMany()
            .HasForeignKey(model => model.CarModelId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Property<uint>("Version").IsRowVersion();
    }
}