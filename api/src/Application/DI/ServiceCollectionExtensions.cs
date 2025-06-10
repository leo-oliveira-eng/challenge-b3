using Application.Services;
using Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;
public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this ServiceCollection services)
    {
        services.AddScoped<IInvestmentApplicationService, InvestmentApplicationService>();
    }
}
