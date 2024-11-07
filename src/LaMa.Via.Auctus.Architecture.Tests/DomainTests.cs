using System.Reflection;
using LaMa.Via.Auctus.Domain.Abstractions;
using NetArchTest.Rules;

namespace LaMa.Via.Auctus.Architecture.Tests;

public class DomainTests
{
    private readonly Assembly DomainAssembly = ViaAuctusAssemblies.DomainAssembly;
    [Fact]
    public void GivenDomainEventsShouldBeSealed()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void GivenDomainEventsShouldHaveNameEndingWithDomainEvent()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void GivenEntitiesShouldHavePrivateParameterlessConstructor()
    {
        var entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity<>))
            .And()
            .AreNotAbstract()
            .GetTypes();

        var failingTypes = new List<Type>();
        foreach (Type entityType in entityTypes)
        {
            ConstructorInfo[] constructors = entityType.GetConstructors(BindingFlags.NonPublic |
                                                                        BindingFlags.Instance);

            if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
            {
                failingTypes.Add(entityType);
            }
        }

        failingTypes.Should().BeEmpty();
    }
}