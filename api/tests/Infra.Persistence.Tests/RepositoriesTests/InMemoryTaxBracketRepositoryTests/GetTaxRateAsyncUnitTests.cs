using Infra.Persistence.Repositories;
using Shouldly;

namespace Infra.Persistence.Tests.RepositoriesTests.InMemoryTaxBracketRepositoryTests;

public class GetTaxRateAsyncUnitTests
{
    [Theory]
    [InlineData(1, 0.225)]
    [InlineData(6, 0.225)]
    [InlineData(10, 0.20)]
    [InlineData(12, 0.20)]
    [InlineData(13, 0.175)]
    [InlineData(24, 0.175)]
    [InlineData(25, 0.15)]
    [InlineData(36, 0.15)]
    [InlineData(int.MaxValue, 0.15)]
    public async Task GetTaxRateAsync_ShouldReturnCorrectTaxRate(int duration, decimal expectedTaxRate)
    {
        // Arrange
        var repository = new InMemoryTaxBracketRepository();

        // Act
        var taxRate = await repository.GetTaxRateAsync(duration);

        // Assert
        taxRate.ShouldBe(expectedTaxRate);
    }
}
