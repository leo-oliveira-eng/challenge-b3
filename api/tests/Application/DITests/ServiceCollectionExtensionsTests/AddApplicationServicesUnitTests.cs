using Application.DI;
using Application.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Application.Tests.DITests.ServiceCollectionExtensionsTests;

public class AddApplicationServicesUnitTests
{
    [Theory]
    [InlineData(typeof(IInvestmentApplicationService), ServiceLifetime.Scoped)]
    public void AddApplicationServices_ShouldRegisterServicesCorrectly(Type serviceType, ServiceLifetime lifetime)
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();
        
        // Assert
        services.ShouldContain(service => 
            service.ServiceType.Equals(serviceType) && 
            service.Lifetime.Equals(lifetime));
    }
}
