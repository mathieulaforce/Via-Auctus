using LaMa.Via.Auctus.Domain.CarManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement.Configuration.Shared;

public static class EngineConfiguration
{
    internal static void RegisterEngineEfficiencyValueObject<T>(
        this OwnedNavigationBuilder<T, EngineEfficiency> efficiencyBuilder) where T : class
    {
        efficiencyBuilder.Property(e => e.Value)
            .HasColumnName("EngineEfficiencyValue");
        efficiencyBuilder.Property(e => e.Unit)
            .HasColumnName("EngineEfficiencyUnit")
            .HasMaxLength(20);
    }
}