using Domain.Investment.Commands;
using Domain.Investment.Commands.Handlers;
using Domain.Investment.Documents;
using Domain.Investment.Dtos;
using Domain.Investment.Services.Contracts;
using Domain.Taxation.Repositories;
using Funcfy.Monads;
using Moq;
using TestSupport.DomainFakers;

namespace Domain.Tests.InvestmentTests.CommandsTests.InvestmentSimulationCommandTests.CommandHandlerTests;

public class HandleUnitTests
{
    private readonly Mock<ITaxBracketRepository> _taxBracketRepositoryMock = new();
    private readonly Mock<ICdbSimulationService> _cdbSimulationServiceMock = new();
    private readonly Mock<IYieldRatesProvider> _yieldRatesProviderMock = new();

    private readonly InvestmentSimulationCommandHandler _handler;

    public HandleUnitTests()
    {
        _handler = new InvestmentSimulationCommandHandler(
            _taxBracketRepositoryMock.Object,
            _yieldRatesProviderMock.Object,
            _cdbSimulationServiceMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenCommandValidationFails()
    {
        // Arrange
        var command = new InvestmentSimulationCommandFake(amount: 0).Generate();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Failed.ShouldBeTrue();
        result.Messages.ShouldContain(message => message.Code!.Equals("BR-001") && message.Content.Equals("Amount must be greater than zero."));
    }

    [Fact]
    public async Task Handle_WhenTokenHasBeenCanceled_ShouldThrowAnException()
    {
        // Arrange
        var request = new InvestmentSimulationCommandFake().Generate();
        var tokenSource = new CancellationTokenSource();
        tokenSource.Cancel();

        // Act
        var act = () => _handler.Handle(request, tokenSource.Token);

        // Assert
        await act.ShouldThrowAsync<OperationCanceledException>();

    }

    [Fact]
    public async Task Handle_WhenTaxRateNotFound_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new InvestmentSimulationCommandFake().Generate();

        _taxBracketRepositoryMock
            .Setup(x => x.GetTaxRateAsync(It.IsAny<int>()))
            .ReturnsAsync(Maybe<decimal>.Empty());

        _yieldRatesProviderMock
            .Setup(x => x.GetAsync())
            .ReturnsAsync(Maybe<YieldRates>.Empty());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Failed.ShouldBeTrue();
        result.Messages.ShouldContain(message => message.Code!.Equals("BR-003") && message.Content.Equals("Tax rate not found for the specified duration."));
    }

    [Fact]
    public async Task Handle_WhenYieldRatesNotFound_ShouldReturnServerError()
    {
        // Arrange
        var request = new InvestmentSimulationCommandFake().Generate();

        _taxBracketRepositoryMock
            .Setup(x => x.GetTaxRateAsync(It.IsAny<int>()))
            .ReturnsAsync(0.20m);

        _yieldRatesProviderMock
            .Setup(x => x.GetAsync())
            .ReturnsAsync(Maybe<YieldRates>.Empty());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Failed.ShouldBeTrue();
        result.Messages.ShouldContain(message => message.Code!.Equals("SE-001") && message.Content.Equals("Yield rates not found."));
    }

    [Fact]
    public async Task Handle_WhenValidCommand_ShouldReturnSimulationResult()
    {
        // Arrange
        var request = new InvestmentSimulationCommand(amount: 1000, duration: 12);
        var taxRate = 0.5m;
        var yieldRates = new YieldRates(cdi: 0.01m, tb: 2m);
        var expectedResult = new CdbSimulationResultFake(request.Amount, request.Duration, taxRate, yieldRates.Cdi, yieldRates.Tb).Generate();

        _taxBracketRepositoryMock
            .Setup(x => x.GetTaxRateAsync(It.IsAny<int>()))
            .ReturnsAsync(taxRate)
            .Verifiable();

        _yieldRatesProviderMock
            .Setup(x => x.GetAsync())
            .ReturnsAsync(yieldRates)
            .Verifiable();

        _cdbSimulationServiceMock.Setup(service => service.Simulate(It.IsAny<CdbSimulationParamsDto>()))
            .Returns(expectedResult)
            .Verifiable();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccessful.ShouldBeTrue();
        result.Data.Value.ShouldBeEquivalentTo(expectedResult);
        result.Messages.ShouldBeEmpty();
    }
}
