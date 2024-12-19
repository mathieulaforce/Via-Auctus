using FakeItEasy;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Infrastructure;
using MediatR;
using NetArchTest.Rules;

namespace LaMa.Via_Auctus.Infrastructure.Tests.Abstractions;

public class WriteRepositoryTests
{
    [Fact]
    public void AllWriteRepositoryConstructorsCanBeInstantiatedWithWriteContext()
    {
        var infrastructureAssembly = typeof(ApplicationWriteDbContext).Assembly;
        var publisher = A.Fake<IPublisher>();
        var context = ApplicationContextTestFactory.CreateWriteContext(publisher);
        var writeRepositories = Types.InAssembly(infrastructureAssembly)
            .That()
            .AreClasses()
            .And()
            .ImplementInterface(typeof(IWriteRepository))
            .And()
            .AreNotAbstract()
            .GetTypes();

        foreach (var writeRepo in writeRepositories)
        {
            Activator.CreateInstance(writeRepo, context);
        }
    }
}