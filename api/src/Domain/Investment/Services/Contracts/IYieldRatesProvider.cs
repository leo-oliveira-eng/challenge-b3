using Domain.Investment.Documents;
using Funcfy.Monads;

namespace Domain.Investment.Services.Contracts;

public interface IYieldRatesProvider
{
    Task<Maybe<YieldRates>> GetAsync();
}
