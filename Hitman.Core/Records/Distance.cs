using System.Text.Json.Serialization;

namespace Hitman.Core.Records;

public record Distance
{
    [JsonPropertyName("value")] public string Value { get; set; }
}