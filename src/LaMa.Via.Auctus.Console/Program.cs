// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using LaMa.Via.Auctus.Application;
using LaMa.Via.Auctus.Application.CarManagement.Cars.GetCar;
using LaMa.Via.Auctus.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
using var host = builder.Build();
await Run(host.Services);
await host.RunAsync();

static async Task Run(IServiceProvider hostProvider)
{
    using var serviceScope = hostProvider.CreateScope();
    var provider = serviceScope.ServiceProvider;
    var mediator = provider.GetRequiredService<IMediator>();
    var a = Guid.Parse("da25b825-fcbe-40a5-bd66-ac18b1956124");
    var b = Guid.Parse("ac5c9cc5-e404-4da6-94e4-ad5f228303e6");

    var query = new GetCarQuery
    {
        CarId = a
    };
    var result = await mediator.Send(query);
    Console.WriteLine(JsonSerializer.Serialize(result.Value, new JsonSerializerOptions { WriteIndented = true }));

    query = new GetCarQuery
    {
        CarId = b
    };
    result = await mediator.Send(query);
    Console.WriteLine(JsonSerializer.Serialize(result.Value, new JsonSerializerOptions { WriteIndented = true }));
    Console.ReadKey();
}