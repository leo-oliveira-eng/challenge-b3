﻿using System.Text.Json.Serialization;

namespace Acceptance.Tests.DataContracts;

internal class EchoResponseMessage
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("version")]
    public string? Version { get; set; }
}
