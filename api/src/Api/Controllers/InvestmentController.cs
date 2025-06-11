using Application.Services.Contracts;
using DataContracts.Requests;
using DataContracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class InvestmentController : AbstractController
{
    private readonly IInvestmentApplicationService _investmentApplicationService;

    public InvestmentController(IInvestmentApplicationService investmentApplicationService)
    {
        _investmentApplicationService = investmentApplicationService;
    }

    [HttpPost("simulate"), ProducesResponseType(typeof(CdbSimulationResponseMessage), 200)]
    public async Task<IActionResult> Simulate([FromBody] InvestmentSimulationRequestMessage requestMessage, CancellationToken cancellationToken)
        => await ExecuteAsync(() => _investmentApplicationService.SimulateAsync(requestMessage, cancellationToken));
}
