using Domain.Investment.Commands;
using Domain.Investment.Commands.Handlers;
using Domain.Investment.Documents;
using Domain.Investment.Services;
using Domain.Investment.Services.Contracts;
using Funcfy.Monads;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.DI;

public static class ServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<InvestmentSimulationCommand, Result<CdbSimulationResult>>, InvestmentSimulationCommandHandler>();
        services.AddTransient<ICdbSimulationService, CdbSimulationService>();
        services.AddTransient<IYieldRatesProvider, FixedYieldRatesProvider>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
    }
}
