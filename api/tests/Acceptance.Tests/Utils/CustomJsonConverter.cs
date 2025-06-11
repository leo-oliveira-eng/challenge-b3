using System.Text.Json;
using System.Text.Json.Serialization;

namespace Acceptance.Tests.Utils;
internal static class CustomJsonConverter
{
    internal static T? DeserializeObject<T>(string json)
    {
        JsonSerializerOptions jsonSerializerSettings = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        return JsonSerializer.Deserialize<T>(json, jsonSerializerSettings);
    }
}
