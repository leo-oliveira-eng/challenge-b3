namespace Domain.Investment.Documents;

public sealed record CdbSimulationResult(
    decimal InitialAmount, 
    decimal GrossAmount, 
    decimal NetAmount, 
    decimal TaxAmount, 
    decimal TaxRate, 
    int Duration, 
    decimal MonthlyRate, 
    decimal TotalInterest)
{ }
