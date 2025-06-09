using Domain.Investment.Documents;

namespace TestSupport.DomainFakers;

public sealed class CdbSimulationResultFake : Faker<CdbSimulationResult>
{
    public CdbSimulationResultFake(decimal? initialAmount = null,
        int? duration = null,
        decimal? taxRate = 0.2m,
        decimal? cdb = 0.01m,
        decimal? tb = 1.1m)
    {
        CustomInstantiator(f =>
        {
            var _initialAmount = initialAmount ?? f.Finance.Amount(1000, 10000);
            var _duration = duration ?? f.Random.Int(1, 60);
            var _taxRate = taxRate ?? 0.2m;
            var _monthlyRate = (cdb ?? 0.01m) * (tb ?? 1.1m);
            var _grossAmount = _initialAmount * (decimal)Math.Pow((double)(1 + _monthlyRate), _duration);
            var _totalInterest = _grossAmount - _initialAmount;
            var _taxAmount = _totalInterest * _taxRate;
            var _netAmount = _grossAmount - _taxAmount;

            return new CdbSimulationResult(
                _initialAmount,
                _grossAmount,
                _netAmount,
                _taxAmount,
                _taxRate,
                _duration,
                _monthlyRate,
                _totalInterest
            );
        });
    }
}
