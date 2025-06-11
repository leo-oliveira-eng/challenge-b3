using System.Text.Json.Serialization;

namespace Acceptance.Tests.DataContracts;

public class Maybe<T> 
{
    [JsonPropertyName("isFull")]
    public bool IsFull { get; set; }

    [JsonPropertyName("isEmpty")]
    public bool IsEmpty { get; set; }

    [JsonPropertyName("value")]
    public T? Value { get; set; }
}
