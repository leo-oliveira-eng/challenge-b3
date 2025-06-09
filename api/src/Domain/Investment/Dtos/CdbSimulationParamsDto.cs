using Domain.Investment.Commands;
using Domain.Investment.Documents;

namespace Domain.Investment.Dtos;

public sealed class CdbSimulationParamsDto(InvestmentSimulationCommand command, YieldRates yieldRates, decimal taxRate)
{
    public decimal InitialAmount { get; init; } = command.Amount;
    public int DurationInMonths { get; init; } = command.Duration;
    public decimal TaxRate { get; init; } = taxRate;
    public decimal Cdi { get; init; } = yieldRates.Cdi;
    public decimal Tb { get; init; } = yieldRates.Tb;
}
