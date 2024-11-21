using LaMa.Via.Auctus.Application.CarManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Read;

public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelTypeDao>
{
    public void Configure(EntityTypeBuilder<FuelTypeDao> builder)
    {
        builder.ToTable("FuelTypes");
        builder.HasKey(model => model.Name);
        builder.Property(model => model.Name).HasColumnName("Id");
    }
}