using LaMa.Via.Auctus.Application.CarManagement.CarModels.GetCarModelsByBrand;
using Microsoft.AspNetCore.Mvc;

namespace LaMa.Via.Auctus.Api.Endpoints.CarManagement;

public class CarModelEndpoints : EndpointGroupRegistrar
{
    public override string GroupName => "carmanagement";
    public override string[] Tags => new[] { "Car Management", "Car Models" };

    protected override void Map(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder
            .MapGet(FindByBrand, "car-models/brand/{brandId}");
    }

    private async Task<IResult> FindByBrand(ISender sender, [FromRoute] Guid brandId, CancellationToken token)
    {
        var brands = await sender.Send(new CarModelsByBrandQuery { CarBrandId = brandId }, token);
        if (brands.IsError)
        {
            return Results.Problem(brands.FirstError.Code);
        }

        return Results.Ok(brands.Value);
    }
}