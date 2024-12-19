using LaMa.Via.Auctus.Infrastructure;
using LaMa.Via.Auctus.Infrastructure.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via_Auctus.Infrastructure.Tests;

public static class ApplicationContextTestFactory
{
    public static ApplicationWriteDbContext CreateWriteContext(IPublisher publisher)
    {
        var dbContextOptions =
            new DbContextOptionsBuilder<ApplicationWriteDbContext>().UseInMemoryDatabase("WriteTest");
        var context = new ApplicationWriteDbContext(dbContextOptions.Options, new DomainEventInterceptor(publisher));
        context.Database.EnsureCreated();
        return context;
    }

    public static ApplicationReadDbContext CreateReadContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationReadDbContext>().UseInMemoryDatabase("ReadTest");
        var context = new ApplicationReadDbContext(dbContextOptions.Options);
        context.Database.EnsureCreated();
        return context;
    }
}