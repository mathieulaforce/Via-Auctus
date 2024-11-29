using System.Reflection;
using LaMa.Via.Auctus.Api.Endpoints;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LaMa.Via.Auctus.Api.Configuration;

public static class EndpointMapper
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        var endpointRegistrarType = typeof(IEndpointGroupRegistrar);

        var endpointRegistrars = executingAssembly.DefinedTypes.Where(type =>
                type is { IsAbstract: false, IsClass: true, IsInterface: false }
                && type.ImplementedInterfaces.Contains(endpointRegistrarType))
            .Select(type => ServiceDescriptor.Transient(endpointRegistrarType, type));

        services.TryAddEnumerable(endpointRegistrars);

        return services;
    }

    public static IApplicationBuilder UseLaMaEndpoints(this WebApplication app, IEndpointRouteBuilder routeBuilder)
    {
        var endpointRegistrars = app.Services.GetServices<IEndpointGroupRegistrar>();
        foreach (var endpoint in endpointRegistrars)
        {
            endpoint.RegisterEndpoints(routeBuilder);
        }

        return app;
    }
}