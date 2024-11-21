using System.Reflection;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.Abstractions;
using LaMa.Via.Auctus.Infrastructure;

namespace LaMa.Via.Auctus.Architecture.Tests;

public static class ViaAuctusAssemblies
{
    public static readonly Assembly DomainAssembly = typeof(Entity<>).Assembly;
    public static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;
    public static readonly Assembly InfrastructureAssembly = typeof(ApplicationWriteDbContext).Assembly;
}