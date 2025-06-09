using Domain.Investment.Services;

namespace Domain.Tests.InvestmentTests.ServicesTests.FixedYieldRatesProviderTests;
public class GetAsyncUnitTests
{
    [Fact]
    public async Task GetAsync_ShouldReturnFixedYieldRates_WhenCalled()
    {
        // Arrange
        var provider = new FixedYieldRatesProvider();
        
        // Act
        var result = await provider.GetAsync();
        
        // Assert
        result.IsFull.ShouldBeTrue();
        result.Value.Cdi.ShouldBe(0.009m);
        result.Value.Tb.ShouldBe(1.08m);
    }
}
