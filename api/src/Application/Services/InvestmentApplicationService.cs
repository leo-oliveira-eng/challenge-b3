using Application.Mappers;
using Application.Services.Contracts;
using DataContracts.Requests;
using DataContracts.Responses;
using DomAid.Mediator.Contracts;
using Domain.Investment.Commands;
using Domain.Investment.Documents;
using Funcfy.Monads;
using Funcfy.Monads.Extensions;

namespace Application.Services;

public class InvestmentApplicationService(IMediatorHandler mediator) : IInvestmentApplicationService
{
    public async Task<Result<CdbSimulationResponseMessage>> SimulateAsync(InvestmentSimulationRequestMessage request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = Result<CdbSimulationResponseMessage>.Create();

        if (request is null)
            return result.WithBadRequest("Request cannot be null.", "BR-004", nameof(SimulateAsync));

        var command = new InvestmentSimulationCommand(request.Amount, request.Duration);

        var commandResult = await mediator.SendAsync<InvestmentSimulationCommand, Result<CdbSimulationResult>>(command);

        if (commandResult.Failed)
            return result.MergeMessagesFrom(commandResult);

        return result.SetValue(commandResult.Data.ToCdbSimulationResponseMessage());
    }
}
