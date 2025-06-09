using Funcfy.Monads;

namespace Domain.Investment.Documents;

public readonly struct YieldRates(decimal cdi, decimal tb)
{
    public decimal Cdi { get; } = cdi;
    public decimal Tb { get; } = tb;

    public static implicit operator YieldRates(Maybe<YieldRates> maybe) => maybe.Value;
}
