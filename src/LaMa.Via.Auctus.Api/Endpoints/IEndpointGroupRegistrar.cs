namespace LaMa.Via.Auctus.Api.Endpoints;

public interface IEndpointGroupRegistrar
{
    string GroupName { get; }
    string[] Tags { get; }
    void RegisterEndpoints(IEndpointRouteBuilder routeBuilder);
}

public abstract class EndpointGroupRegistrar : IEndpointGroupRegistrar
{
    public abstract string GroupName { get; }
    public abstract string[] Tags { get; }

    public void RegisterEndpoints(IEndpointRouteBuilder routeBuilder)
    {
        Map(routeBuilder
            .MapGroup(GroupName)
            .WithTags(Tags)
            .WithOpenApi());
    }

    protected abstract void Map(IEndpointRouteBuilder routeBuilder);
}