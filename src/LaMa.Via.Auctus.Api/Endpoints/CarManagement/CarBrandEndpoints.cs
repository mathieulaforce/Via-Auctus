using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Lookup;

namespace LaMa.Via.Auctus.Api.Endpoints.CarManagement;

public class CarBrandEndpoints : EndpointGroupRegistrar
{
    public override string GroupName => "carmanagement";
    public override string[] Tags => new[] { "Car Management", "Car Brands" };

    protected override void Map(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet(Lookup, "car-brands/lookup");
    }

    private async Task<IResult> Lookup(ISender sender, CancellationToken token)
    {
        var brands = await sender.Send(new LookupCarBrandQuery(), token);
        if (brands.IsError)
        {
            return Results.Problem(brands.FirstError.Code);
        }

        return Results.Ok(brands.Value);
    }
    // private async Task<IResult> Lookup(ISender sender,[FromRoute] Guid id ,CancellationToken token)
    // {
    //     var car =await sender.Send(new GetCarQuery
    //     {
    //         CarId = id
    //     }, token);
    //
    //     if (car.Value is null)
    //     {
    //         return Results.Problem(  detail: $"Car with id {id} not found", statusCode: 404);
    //     }
    //
    //     if (car.IsError)
    //     {
    //         return Results.Problem(car.FirstError.Code);
    //     }
    //     
    //     return Results.Ok(car.Value);
    // }
}