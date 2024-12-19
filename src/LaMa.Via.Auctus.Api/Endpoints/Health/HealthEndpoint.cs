using System.Reflection;
using LaMa.Via.Auctus.Application.Common;

namespace LaMa.Via.Auctus.Api.Endpoints.Health;

public class HealthEndpoint : EndpointGroupRegistrar
{
    public override string GroupName => "health";
    public override string[] Tags => ["Health"];

    protected override void Map(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet(HearthBeat)
            ;
    }

    private async Task<IResult> HearthBeat(IDateTimeProvider dateTimeProvider, CancellationToken token)
    {
        var timeRequested = dateTimeProvider.GetCurrentInstant();
        var version = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "?";

        var result = await Task.FromResult(new
        {
            hearthBeat = timeRequested, version
        });
        return Results.Ok(result);
    }
}