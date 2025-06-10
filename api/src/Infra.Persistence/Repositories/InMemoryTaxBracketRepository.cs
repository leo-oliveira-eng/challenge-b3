using Domain.Taxation.Documents;
using Domain.Taxation.Repositories;
using Funcfy.Monads;

namespace Infra.Persistence.Repositories;

public class InMemoryTaxBracketRepository : ITaxBracketRepository
{
    public async Task<Maybe<decimal>> GetTaxRateAsync(int months)
    {
        var bracket = brackets.First(b => months <= b.MaxMonths);
        return await Task.FromResult(bracket.Rate);
    }

    private readonly List<TaxBracket> brackets =
    [
        new TaxBracket(0.225m, 6),
        new TaxBracket(0.20m, 12),
        new TaxBracket(0.175m, 24),
        new TaxBracket(0.15m, int.MaxValue)
    ];
}
