using Domain.Investment.Documents;
using Domain.Investment.Services.Contracts;
using Domain.Taxation.Repositories;
using Funcfy.Monads;
using Funcfy.Monads.Extensions;
using MediatR;

namespace Domain.Investment.Commands.Handlers;

public sealed class InvestmentSimulationCommandHandler(ITaxBracketRepository taxBracketRepository, IYieldRatesProvider yieldRatesProvider, ICdbSimulationService simulationService) 
    : IRequestHandler<InvestmentSimulationCommand, Result<CdbSimulationResult>>
{
    public async Task<Result<CdbSimulationResult>> Handle(InvestmentSimulationCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = Result<CdbSimulationResult>.Create();

        var commandValidation = request.Validate();

        if (commandValidation.Failed)
            return result.MergeMessagesFrom(commandValidation);

        var taxes = await taxBracketRepository.GetTaxRateAsync(request.Duration);

        if (taxes.IsEmpty)
            return result.WithBadRequest("Tax rate not found for the specified duration.", "BR-003", nameof(InvestmentSimulationCommandHandler));

        var yieldRates = await yieldRatesProvider.GetAsync();

        if (yieldRates.IsEmpty)
            return result.WithServerError("Yield rates not found.", "SE-001", nameof(InvestmentSimulationCommandHandler));

        return simulationService.Simulate(new Dtos.CdbSimulationParamsDto(request, yieldRates, taxes.Value));
    }
}
