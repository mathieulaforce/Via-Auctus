using System.Reflection;
using NetArchTest.Rules;

namespace LaMa.Via.Auctus.Architecture.Tests;

public class LayerTests
{
    private static Assembly ApplicationAssembly = ViaAuctusAssemblies.ApplicationAssembly;
    private static Assembly DomainAssembly = ViaAuctusAssemblies.DomainAssembly;
    private static Assembly InfrastructureAssembly = ViaAuctusAssemblies.InfrastructureAssembly;
    [Fact]
    public void DomainLayerShouldNotHaveDependencyOnApplicationLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainLayerShouldNotHaveDependencyOnInfrastructureLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayerShouldNotHaveDependencyOnInfrastructureLayer()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    // [Fact]
    // public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    // {
    //     var result = Types.InAssembly(ApplicationAssembly)
    //         .Should()
    //         .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
    //         .GetResult();
    //
    //     result.IsSuccessful.Should().BeTrue();
    // }

    // [Fact]
    // public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    // {
    //     var result = Types.InAssembly(InfrastructureAssembly)
    //         .Should()
    //         .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
    //         .GetResult();
    //
    //     result.IsSuccessful.Should().BeTrue();
    // }
}