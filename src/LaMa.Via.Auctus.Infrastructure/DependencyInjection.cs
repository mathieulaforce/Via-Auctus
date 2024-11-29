using System.Reflection;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.Common; 
using LaMa.Via.Auctus.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace LaMa.Via.Auctus.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        ConfigureDatabase(services, configuration);
        ConfigureRepositories(services, assembly);

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());
       
        services.AddSingleton<IClock>(SystemClock.Instance);
        services.AddSingleton<ZonedClock>((sp) =>
        {
            var clock = sp.GetRequiredService<IClock>();
            return new ZonedClock(clock, DateTimeZone.Utc, CalendarSystem.Iso);
        });
  
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<DomainEventInterceptor>();

        return services;
    }

    private static void ConfigureRepositories(IServiceCollection services, Assembly assembly)
    {
        var writeRepositoryType = typeof(IWriteRepository);
        var writeRepositories = assembly.DefinedTypes
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false, IsClass: true }
                && type.ImplementedInterfaces.Contains(writeRepositoryType)
            );

        foreach (var writeRepository in writeRepositories)
        {
            var implementedInterface =
                writeRepository.ImplementedInterfaces.Single(type => type.Name.Contains(writeRepository.Name));
            services.AddScoped(implementedInterface, writeRepository);
        }

        var readRepositoryType = typeof(IReadRepository);
        var readRepositories = assembly.DefinedTypes
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false, IsClass: true }
                && type.ImplementedInterfaces.Contains(readRepositoryType)
            );

        foreach (var readRepository in readRepositories)
        {
            var implementedInterface =
                readRepository.ImplementedInterfaces.Single(type => type.Name.Contains(readRepository.Name));
            services.AddScoped(implementedInterface, readRepository);
        }
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var writeConnectionString = configuration.GetConnectionString("WriteDatabase") ??
                                    throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<ApplicationWriteDbContext>(options =>
            options.UseNpgsql(writeConnectionString,
                    builder => builder.MigrationsHistoryTable("__EFMigrationsHistory", Constants.DbSchemaName))
                .ReplaceService<IHistoryRepository, ViaAuctusHistoryRepository>()
        );

        var readConnectionString = configuration.GetConnectionString("ReadDatabase") ??
                                   throw new ArgumentNullException(nameof(configuration));
        services.AddDbContext<ApplicationReadDbContext>(options =>
            options.UseNpgsql(readConnectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
    }
}