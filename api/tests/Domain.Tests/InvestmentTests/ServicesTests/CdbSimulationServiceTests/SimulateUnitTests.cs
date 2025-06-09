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
        result.GrossAmount.ShouldBe(1123.0820949653m, 0.0000000001m);
        result.NetAmount.ShouldBe(1104.6197807205m, 0.0000000001m);
        result.TaxAmount.ShouldBe(18.4623142448m, 0.0000000001m);
        result.TaxRate.ShouldBe(taxRate);
        result.Duration.ShouldBe(durationInMonths);
        result.MonthlyRate.ShouldBe(cdi * tb);
        result.TotalInterest.ShouldBe(123.0820949653m, 0.0000000001m);
    }
}
