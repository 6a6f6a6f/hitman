using System.Text.Json.Serialization;

namespace Hitman.Core.Records;

public record FollowingInfo
{
    [JsonPropertyName("followingType")] public string FollowingType { get; set; }
    [JsonPropertyName("entityUrn")] public string EntityUrn { get; set; }
    [JsonPropertyName("followerCount")] public long FollowerCount { get; set; }
    [JsonPropertyName("following")] public bool Following { get; set; }
    [JsonPropertyName("trackingUrn")] public string TrackingUrn { get; set; }
}