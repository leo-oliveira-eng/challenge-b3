using System.Text.Json;
using System.Text.Json.Serialization;

namespace Acceptance.Tests.Utils;

internal static class CustomJsonConverter
{
    private static readonly JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    internal static T? DeserializeObject<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, Options);
    }
}
