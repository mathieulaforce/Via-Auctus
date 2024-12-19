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
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });


        var origins = config.GetSection("CORS:Origins").GetChildren().Select(c => c.Value).ToArray();
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins((origins ?? [])!);
                    builder.SetIsOriginAllowed(origin => true);
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
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