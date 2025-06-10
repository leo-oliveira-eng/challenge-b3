using Domain.Shared;
using Infra.CrossCutting.DI;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Infra.CrossCutting.Tests.DITests.ServiceCollectionExtensionsTests;

public class AddCrossCuttingServicesUnitTests
{
    [Theory]
    [InlineData(typeof(IMediatorHandler), ServiceLifetime.Scoped)]
    public void AddCrossCuttingServices_ShouldRegisterServicesCorrectly(Type serviceType, ServiceLifetime lifetime)
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddCrossCuttingServices();
        
        // Assert
        services.ShouldContain(service => 
            service.ServiceType.Equals(serviceType) && 
            service.Lifetime.Equals(lifetime));
    }
}
