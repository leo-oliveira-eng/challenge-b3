using DataContracts.Responses;
using Domain.Investment.Documents;
using Funcfy.Monads;

namespace Application.Mappers;

internal static class CdbSimulationResponseMessageMapper
{
    internal static CdbSimulationResponseMessage ToCdbSimulationResponseMessage(this Maybe<CdbSimulationResult> result)
    {
        var simulationResult = result.Value;

        return new()
        {
            GrossAmount = simulationResult!.GrossAmount,
            InitialAmount = simulationResult.InitialAmount,
            NetAmount = simulationResult.NetAmount,
            TaxAmount = simulationResult.TaxAmount,
            TaxRate = simulationResult.TaxRate,
            Duration = simulationResult.Duration,
            MonthlyRate = simulationResult.MonthlyRate,
            TotalInterest = simulationResult.TotalInterest
        };
    }
}
