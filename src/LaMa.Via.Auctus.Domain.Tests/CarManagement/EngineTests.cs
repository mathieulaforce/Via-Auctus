using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.CarManagement.Events;
using LaMa.Via.Auctus.Domain.Tests.Helpers;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement;

public class EngineTests
{
    [Fact]
    public void CreateNewEnginePublishesEvent()
    { 
        var engine = Engine.Create("1.5 TSI EVO", FuelType.Create("Gasoline").Value, 150, 250, EngineEfficiency.LPer100Km(6.9m));

        var domainEvent = engine.ContainsOneDomainEventOfType<EngineCreatedDomainEvent>();
        
        domainEvent.Should().NotBeNull();
        engine.Id.Should().Be(domainEvent.EngineId);
        engine.FuelType.Should().Be(FuelType.Create("Gasoline").Value);
        engine.Efficiency.Should().Be(EngineEfficiency.LPer100Km(6.9m));
        engine.HorsePower.Should().Be(150);
        engine.Torque.Should().Be(250);
    }
}