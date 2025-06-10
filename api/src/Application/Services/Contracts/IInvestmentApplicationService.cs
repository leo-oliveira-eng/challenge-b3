using DataContracts.Requests;
using DataContracts.Responses;
using Funcfy.Monads;

namespace Application.Services.Contracts;
public interface IInvestmentApplicationService
{
    Task<Result<CdbSimulationResponseMessage>> SimulateAsync(InvestmentSimulationRequestMessage request, CancellationToken cancellationToken);
}
