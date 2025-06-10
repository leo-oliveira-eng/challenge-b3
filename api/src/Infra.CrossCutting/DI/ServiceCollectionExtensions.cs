using Domain.Shared;
using Infra.CrossCutting.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.DI;
public static class ServiceCollectionExtensions
{
    public static void AddCrossCuttingServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, InternalBus>();        
    }
}
