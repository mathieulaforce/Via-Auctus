using System.Reflection;
using FakeItEasy;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Infrastructure;
using LaMa.Via.Auctus.Infrastructure.Abstractions;
using MediatR;
using NetArchTest.Rules;

namespace LaMa.Via_Auctus.Infrastructure.Tests.Abstractions;

public class ReadRepositoryTests
{ 
    [Fact]
    public void AllReadRepositoryConstructorsCanBeInstantiatedWithReadContext()
    {
        Assembly infrastructureAssembly = typeof(ApplicationReadDbContext).Assembly; 
        
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
            Activator.CreateInstance(readRepo,args:context );
        }
    }
}