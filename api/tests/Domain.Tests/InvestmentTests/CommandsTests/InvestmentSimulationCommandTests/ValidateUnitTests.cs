using Domain.Investment.Commands;

namespace Domain.Tests.InvestmentTests.CommandsTests.InvestmentSimulationCommandTests;

public class ValidateUnitTests
{
    [Fact]
    public void Validate_ValidCommand_ReturnsSuccessResult()
    {
        // Arrange
        var command = new InvestmentSimulationCommand(amount: 1000, duration: 5);

        // Act
        var result = command.Validate();

        // Assert
        result.IsSuccessful.ShouldBeTrue();
        result.Messages.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_InvalidCommand_ReturnsBadRequestResult()
    {
        // Arrange
        var command = new InvestmentSimulationCommand(amount: 0, duration: 0);

        // Act
        var result = command.Validate();

        // Assert
        result.IsSuccessful.ShouldBeFalse();
        result.Messages.ShouldNotBeEmpty();
        result.Messages.ShouldContain(m => m.Content == "Amount must be greater than zero." && m.Code == "BR-001");
        result.Messages.ShouldContain(m => m.Content == "Duration must be greater than 1." && m.Code == "BR-002");
    }
}
