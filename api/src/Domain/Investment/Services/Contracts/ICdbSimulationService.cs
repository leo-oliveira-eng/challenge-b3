using Domain.Investment.Documents;
using Domain.Investment.Dtos;

namespace Domain.Investment.Services.Contracts;

public  interface ICdbSimulationService
{
    CdbSimulationResult Simulate(CdbSimulationParamsDto dto);
}
