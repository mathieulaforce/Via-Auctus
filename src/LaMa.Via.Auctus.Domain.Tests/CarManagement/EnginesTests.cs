using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Domain.Tests.CarManagement.ObjectMothers;

namespace LaMa.Via.Auctus.Domain.Tests.CarManagement;

public class EnginesTests
{
    [Fact]
    public void GivenNewEngineWhenAddThenAddsEngine()
    {
        var engines = Engines.Empty;
        var engine = EngineObjectMother.UnknownElectricEngine;
        
        engines.AddEngine(engine);
        
        engines.Should().NotBeEmpty();
        engines.Should().Contain(engine);
    }
    
    [Fact]
    public void GivenEmptyListContainsNoEngine()
    {
        var engines = Engines.Empty;
        engines.Should().BeEmpty();
    }
    
    [Fact]
    public void GivenNewEngineWhenAddThenAddsNewEngine()
    {
        var engines = Engines.Empty;
        var engine = EngineObjectMother.UnknownElectricEngine;
        engines.AddEngine(engine);
        
        var otherEngine = Engine.Create("ABC", FuelType.Create("hybrid"), 1, 2, 3);
        
        engines.Contains(otherEngine).Should().BeFalse();
        engines.Contains(otherEngine.Id).Should().BeFalse();
        engines.CanAddEngine(otherEngine).Should().BeTrue();
        engines.CanAddEngine(otherEngine.Name ,otherEngine.FuelType,otherEngine.HorsePower, otherEngine.Torque,otherEngine.Efficiency ).Should().BeTrue();
    }
    
    [Fact]
    public void GivenExistingEngineWhenAddThenThrowsException()
    {
        var engines = Engines.Empty;
        var engine = EngineObjectMother.UnknownElectricEngine;
        engines.AddEngine(engine);
        var action = () => engines.AddEngine(engine);
        
        engines.CanAddEngine(engine).Should().BeFalse();
        engines.Contains(engine).Should().BeTrue();
        engines.Contains(engine.Id).Should().BeTrue();
        engines.CanAddEngine(engine.Name ,engine.FuelType,engine.HorsePower, engine.Torque,engine.Efficiency ).Should().BeFalse();
        action.Should().Throw<Exception>(); 
    }
 
}