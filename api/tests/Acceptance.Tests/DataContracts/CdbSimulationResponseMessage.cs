using System.Text.Json.Serialization;

namespace Acceptance.Tests.DataContracts;

internal class CdbSimulationResponseMessage
{
    [JsonPropertyName("initialAmount")]
    public decimal InitialAmount { get; set; }

    [JsonPropertyName("grossAmount")]
    public decimal GrossAmount { get; set; }

    [JsonPropertyName("netAmount")]
    public decimal NetAmount { get; set; }

    [JsonPropertyName("taxAmount")]
    public decimal TaxAmount { get; set; }

    [JsonPropertyName("taxRate")]
    public decimal TaxRate { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("monthlyRate")]
    public decimal MonthlyRate { get; set; }

    [JsonPropertyName("totalInterest")]
    public decimal TotalInterest { get; set; }
}
