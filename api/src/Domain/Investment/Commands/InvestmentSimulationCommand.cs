using DomAid.Messaging;
using Domain.Investment.Commands.Validators;
using Domain.Investment.Documents;
using Funcfy.Monads;
using Funcfy.Monads.Extensions;
using MediatR;

namespace Domain.Investment.Commands;

public sealed class InvestmentSimulationCommand(decimal amount, int duration) : Command, IRequest<Result<CdbSimulationResult>>
{
    public int Duration { get; init; } = duration;

    public decimal Amount { get; init; } = amount;

    public override Result Validate()
    {
        var result = Result.Create();

        var validationResult = new InvestmentSimulationCommandValidator().Validate(this);

        validationResult.Errors.ForEach(error =>
            result.WithBadRequest(error.ErrorMessage, error.ErrorCode, nameof(InvestmentSimulationCommandValidator)));

        return result;
    }
}
