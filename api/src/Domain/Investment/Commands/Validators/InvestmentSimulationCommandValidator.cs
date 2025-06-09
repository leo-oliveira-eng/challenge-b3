using FluentValidation;

namespace Domain.Investment.Commands.Validators;

public sealed class InvestmentSimulationCommandValidator : AbstractValidator<InvestmentSimulationCommand>
{
    public InvestmentSimulationCommandValidator()
    {
        RuleFor(command => command.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero.")
            .WithErrorCode("BR-001");
        RuleFor(command => command.Duration)
            .GreaterThan(1)
            .WithMessage("Duration must be greater than 1.")
            .WithErrorCode("BR-002");
    }
}
