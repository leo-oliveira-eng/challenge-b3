using Application.Services;
using DataContracts.Requests;
using DomAid.Mediator.Contracts;
using Domain.Investment.Commands;
using Domain.Investment.Documents;
using Funcfy.Monads;
using Funcfy.Monads.Extensions;
using Moq;
using Shouldly;
using TestSupport.DomainFakers;

namespace Application.Tests.ServicesTests.InvestmentApplicationServiceTests;

public class SimulateAsyncUnitTests
{
    private readonly Mock<IMediatorHandler> mediatorMock = new();
    private readonly InvestmentApplicationService service;

    public SimulateAsyncUnitTests()
    {
        service = new InvestmentApplicationService(mediatorMock.Object);
    }

    [Fact]
    public async Task SimulateAsync_WhenRequestIsNull_ShouldReturnBadRequest()
    {
        // Act
        var result = await service.SimulateAsync(null!, CancellationToken.None);

        // Assert
        result.Failed.ShouldBeTrue();
        result.Messages.Any(m => m.Code == "BR-004").ShouldBeTrue();
    }

    [Fact]
    public async Task SimulateAsync_WhenCancelled_ShouldThrowOperationCanceledException()
    {
        // Arrange
        var request = new InvestmentSimulationRequestMessage { Amount = 1000m, Duration = 12 };
        var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act
        var act = () => service.SimulateAsync(request, cts.Token);

        // Assert
        await act.ShouldThrowAsync<OperationCanceledException>();
    }

    [Fact]
    public async Task SimulateAsync_WhenCommandResultHasError_ShouldReturnMergedResult()
    {
        // Arrange
        var request = new InvestmentSimulationRequestMessage { Amount = 1000m, Duration = 12 };
        var failedResult = Result<CdbSimulationResult>.Create().WithServerError("Any error message", "ERR-001");
        mediatorMock
            .Setup(m => m.SendAsync<InvestmentSimulationCommand, Result<CdbSimulationResult>>(It.IsAny<InvestmentSimulationCommand>()))
            .ReturnsAsync(failedResult);

        // Act
        var result = await service.SimulateAsync(request, CancellationToken.None);

        // Assert
        result.Failed.ShouldBeTrue();
        result.Messages.Any(m => m.Code == "ERR-001").ShouldBeTrue();
    }

    [Fact]
    public async Task SimulateAsync_WhenCommandSucceeds_ShouldReturnSimulationResponse()
    {
        // Arrange
        var request = new InvestmentSimulationRequestMessage { Amount = 1000m, Duration = 12 };
        var simulationResult = new CdbSimulationResultFake().Generate();
        var successResult = Result<CdbSimulationResult>.Success(simulationResult);

        mediatorMock
            .Setup(m => m.SendAsync<InvestmentSimulationCommand, Result<CdbSimulationResult>>(It.IsAny<InvestmentSimulationCommand>()))
            .ReturnsAsync(successResult);

        // Act
        var result = await service.SimulateAsync(request, CancellationToken.None);

        // Assert
        result.Failed.ShouldBeFalse();
        result.Data.Value.ShouldNotBeNull();
        result.Data.Value.GrossAmount.ShouldBe(simulationResult.GrossAmount);
        result.Data.Value.NetAmount.ShouldBe(simulationResult.NetAmount);
        result.Data.Value.Duration.ShouldBe(simulationResult.Duration);
        result.Data.Value.TaxRate.ShouldBe(simulationResult.TaxRate);
        result.Data.Value.TaxAmount.ShouldBe(simulationResult.TaxAmount);
        result.Data.Value.InitialAmount.ShouldBe(simulationResult.InitialAmount);
        result.Data.Value.MonthlyRate.ShouldBe(simulationResult.MonthlyRate);
        result.Data.Value.TotalInterest.ShouldBe(simulationResult.TotalInterest);
    }
}
