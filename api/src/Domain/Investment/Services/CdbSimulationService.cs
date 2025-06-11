using Domain.Investment.Documents;
using Domain.Investment.Dtos;
using Domain.Investment.Services.Contracts;

namespace Domain.Investment.Services;

public sealed class CdbSimulationService : ICdbSimulationService
{
    public CdbSimulationResult Simulate(CdbSimulationParamsDto dto)
    {
        var monthlyRate = dto.Cdi * dto.Tb;
        var grossAmount = CalculateGrossAmount(dto, monthlyRate);
        var totalInterest = grossAmount - dto.InitialAmount;
        var taxAmount = totalInterest * dto.TaxRate;
        var netAmount = grossAmount - taxAmount;

        return new(
            Math.Round(dto.InitialAmount, 2, MidpointRounding.AwayFromZero),
            Math.Round(grossAmount, 2, MidpointRounding.AwayFromZero),
            Math.Round(netAmount, 2, MidpointRounding.AwayFromZero),
            Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero),
            Math.Round(dto.TaxRate, 4, MidpointRounding.AwayFromZero),
            dto.DurationInMonths,
            Math.Round(monthlyRate, 4, MidpointRounding.AwayFromZero),
            Math.Round(totalInterest, 2, MidpointRounding.AwayFromZero)
        );
    }

    private static decimal CalculateGrossAmount(CdbSimulationParamsDto dto, decimal monthlyRate)
    {
        var grossAmount = dto.InitialAmount;

        for (int i = 0; i < dto.DurationInMonths; i++)
            grossAmount *= (1 + monthlyRate);

        return grossAmount;
    }
}
