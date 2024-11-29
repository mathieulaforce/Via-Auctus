using Asp.Versioning;
using LaMa.Via.Auctus.Api.Configuration;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace LaMa.Via.Auctus.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer(); 
        services.AddOpenApi(); 
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1,0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
        
        services.AddProblemDetails(); 
        services.AddEndpoints();
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
        });

        return services;
    }
}