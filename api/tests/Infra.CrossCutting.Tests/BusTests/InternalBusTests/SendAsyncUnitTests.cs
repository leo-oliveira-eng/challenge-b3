using Domain.Investment.Commands;
using Domain.Investment.Documents;
using Funcfy.Monads;
using Infra.CrossCutting.Bus;
using MediatR;
using Moq;
using Shouldly;

namespace Infra.CrossCutting.Tests.BusTests.InternalBusTests;

public class SendAsyncUnitTests
{
    [Fact]
    public async Task SendAsync_WhenSendAsyncIsCalled_ShouldSendCommandByMediator()
    {
        // arrange
        var result = Result<CdbSimulationResult>.Create();
        var mediator = new Mock<IMediator>();
        var bus = new InternalBus(mediator.Object);

        mediator.Setup(m => m.Send(It.IsAny<InvestmentSimulationCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result)
            .Verifiable();

        // act
        var response  = await bus.SendAsync<InvestmentSimulationCommand, Result<CdbSimulationResult>>(new InvestmentSimulationCommand(1000m, 12), CancellationToken.None);

        // assert
        response.ShouldBe(result);
        mediator.Verify();
    }
}
