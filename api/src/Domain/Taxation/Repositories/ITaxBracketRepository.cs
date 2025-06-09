using Funcfy.Monads;

namespace Domain.Taxation.Repositories;

public interface ITaxBracketRepository
{
    Task<Maybe<decimal>> GetTaxRateAsync(int months);
}
