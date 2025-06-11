using Domain.Taxation.Repositories;
using Infra.Persistence.DI;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Infra.Persistence.Tests.DITests.ServiceCollectionExtensionsTests;

public class AddPersistenceServicesUnitTests
{
    [Theory]
    [InlineData(typeof(ITaxBracketRepository), ServiceLifetime.Transient)]
    public void AddPersistenceServices_ShouldRegisterServicesCorrectly(Type serviceType, ServiceLifetime lifetime)
    {
        // Arrange
        var services = new ServiceCollection();
        // Act
        services.AddPersistenceServices();
        
        // Assert
        services.ShouldContain(service => 
            service.ServiceType.Equals(serviceType) && 
            service.Lifetime.Equals(lifetime));
    }
}
