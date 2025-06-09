using Domain.DI;
using Domain.Investment.Commands;
using Domain.Investment.Documents;
using Domain.Investment.Services.Contracts;
using Funcfy.Monads;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Tests.DITests.ServiceCollectionExtensionsTests;
public class AddDomainServicesUnitTests
{
    [Theory]
    [InlineData(typeof(IRequestHandler<InvestmentSimulationCommand, Result<CdbSimulationResult>>), ServiceLifetime.Transient)]
    [InlineData(typeof(ICdbSimulationService), ServiceLifetime.Transient)]
    [InlineData(typeof(IYieldRatesProvider), ServiceLifetime.Transient)]
    public void AddDomainServices_ShouldRegisterServicesCorrectly(Type serviceType, ServiceLifetime lifetime)
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddDomainServices();
        
        // Assert
        services.ShouldContain(service => 
            service.ServiceType.Equals(serviceType) && 
            service.Lifetime.Equals(lifetime));
    }
}
