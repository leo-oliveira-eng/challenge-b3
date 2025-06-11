using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Acceptance.Tests.DataContracts;

internal sealed class InvestmentSimulationRequestMessage
{
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("duration")]
    public int Duration { get; set; }
}
