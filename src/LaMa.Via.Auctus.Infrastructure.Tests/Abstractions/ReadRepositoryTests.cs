using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Infrastructure;
using NetArchTest.Rules;

namespace LaMa.Via_Auctus.Infrastructure.Tests.Abstractions;

public class ReadRepositoryTests
{
    [Fact]
    public void AllReadRepositoryConstructorsCanBeInstantiatedWithReadContext()
    {
        var infrastructureAssembly = typeof(ApplicationReadDbContext).Assembly;

        var context = ApplicationContextTestFactory.CreateReadContext();
        var readRepositories = Types.InAssembly(infrastructureAssembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IReadRepository))
            .And()
            .AreNotAbstract()
            .GetTypes();

        foreach (var readRepo in readRepositories)
        {
            Activator.CreateInstance(readRepo, context);
        }
    }
}