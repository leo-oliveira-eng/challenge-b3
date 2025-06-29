﻿using Domain.Investment.Commands;

namespace TestSupport.DomainFakers;
public class InvestmentSimulationCommandFake : Faker<InvestmentSimulationCommand>
{
    public InvestmentSimulationCommandFake() { }

    public static InvestmentSimulationCommand Create(decimal? amount = null, int? duration = null)
    {
        return new Faker<InvestmentSimulationCommand>()
            .CustomInstantiator(f => new InvestmentSimulationCommand(
                amount ?? f.Finance.Amount(1000, 10000),
                duration ?? f.Random.Int(1, 60)))
            .Generate();
    }
}
