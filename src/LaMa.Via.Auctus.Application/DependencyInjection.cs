using LaMa.Via.Auctus.Application.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace LaMa.Via.Auctus.Application; 

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehaviourPipeline<,>));
        });
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }
}