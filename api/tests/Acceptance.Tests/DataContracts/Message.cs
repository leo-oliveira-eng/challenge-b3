using System.Text.Json.Serialization;

namespace Acceptance.Tests.DataContracts;

public class Message
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("type")]
    public int? Type { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }
}
