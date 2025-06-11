using System.Text.Json.Serialization;

namespace Acceptance.Tests.DataContracts;
public class Result
{
    [JsonPropertyName("messages")]
    public List<Message> Messages { get; set; } = [];

    [JsonPropertyName("failed")]
    public bool Failed { get; set; }

    [JsonPropertyName("isSuccessful")]
    public bool IsSuccessful { get; set; }
}

public class Result<T> : Result
{
    [JsonPropertyName("data")]
    public Maybe<T>? Data { get; set; }
}
