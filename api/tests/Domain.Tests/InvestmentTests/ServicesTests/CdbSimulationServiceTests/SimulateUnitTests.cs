using Domain.Investment.Commands;
using Domain.Investment.Documents;
using Domain.Investment.Dtos;
using Domain.Investment.Services;

namespace Domain.Tests.InvestmentTests.ServicesTests.CdbSimulationServiceTests;

public class SimulateUnitTests
{
    [Fact]
    public void Simulate_ShouldReturnCorrectSimulationResult()
    {
        // Arrange
        var initialAmount = 1000m;
        var durationInMonths = 12;
        var taxRate = 0.15m; 
        var cdi = 0.009m; 
        var tb = 1.08m;
        var dto = new CdbSimulationParamsDto(
            new InvestmentSimulationCommand(initialAmount, durationInMonths),
            new YieldRates(cdi, tb),
            taxRate
        );
        var service = new CdbSimulationService();

        // Act
        var result = service.Simulate(dto);

        // Assert
        result.InitialAmount.ShouldBe(initialAmount);
        result.GrossAmount.ShouldBe(1123.08m);
        result.NetAmount.ShouldBe(1104.62m);
        result.TaxAmount.ShouldBe(18.46m);
        result.TaxRate.ShouldBe(taxRate);
        result.Duration.ShouldBe(durationInMonths);
        result.MonthlyRate.ShouldBe(0.0097m);
        result.TotalInterest.ShouldBe(123.08m);
    }
}
