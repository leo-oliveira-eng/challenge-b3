using Domain.Investment.Documents;
using Domain.Investment.Services.Contracts;
using Funcfy.Monads;

namespace Domain.Investment.Services;

public sealed class FixedYieldRatesProvider : IYieldRatesProvider
{
    public async Task<Maybe<YieldRates>> GetAsync() 
        => await Task.FromResult(new YieldRates(0.009m, 1.08m));
}
