using System.Reflection;
using LaMa.Via.Auctus.Application.Abstractions;
using NetArchTest.Rules;

namespace LaMa.Via.Auctus.Architecture.Tests;

public class InfrastructureTests
{
    private readonly Assembly InfrastructureAssembly = ViaAuctusAssemblies.InfrastructureAssembly;

    [Fact]
    public void GivenWriteRepositoryShouldHaveNameEndingWithWriteRepository()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IWriteRepository))
            .And()
            .AreNotPublic()
            .And()
            .AreNotAbstract()
            .Should()
            .HaveNameEndingWith("WriteRepository")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(string.Join(", ", result.FailingTypeNames ?? []));
    }

    [Fact]
    public void GivenWriteRepositoryShouldNotBePublic()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IWriteRepository))
            .And()
            .AreNotPublic()
            .Should()
            .NotBePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue(string.Join(", ", result.FailingTypeNames ?? []));
    }

    [Fact]
    public void GivenReadRepositoryShouldHaveNameEndingWithWriteRepository()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IReadRepository))
            .And()
            .AreNotPublic()
            .And()
            .AreNotAbstract()
            .Should()
            .HaveNameEndingWith("ReadRepository")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(string.Join(", ", result.FailingTypeNames ?? []));
    }

    [Fact]
    public void GivenReadRepositoryShouldNotBePublic()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IReadRepository))
            .And()
            .AreNotPublic()
            .Should()
            .NotBePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue(string.Join(", ", result.FailingTypeNames ?? []));
    }
}