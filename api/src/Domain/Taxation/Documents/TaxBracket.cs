namespace Domain.Taxation.Documents;

public readonly struct TaxBracket(decimal rate, int maxMonths)
{
    public int MaxMonths { get; } = maxMonths;
    public decimal Rate { get; } = rate;
}
