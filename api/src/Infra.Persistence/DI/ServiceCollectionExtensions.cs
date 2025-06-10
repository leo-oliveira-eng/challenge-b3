using Domain.Taxation.Repositories;
using Infra.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Persistence.DI;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<ITaxBracketRepository, InMemoryTaxBracketRepository>();
    }
}
