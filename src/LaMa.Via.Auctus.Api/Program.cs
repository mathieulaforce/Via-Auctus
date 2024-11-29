using Asp.Versioning;
using LaMa.Via.Auctus.Api;
using LaMa.Via.Auctus.Api.Configuration;
using LaMa.Via.Auctus.Application;
using LaMa.Via.Auctus.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddApi(builder.Configuration);
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
 
var apiVersioning = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();
var apiVersionRouteBuilder = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet(apiVersioning);
app.UseLaMaEndpoints(apiVersionRouteBuilder);

app.Run();

 