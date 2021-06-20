using System.Text.Json.Serialization;

namespace Hitman.Core.Records
{
    public record NetworkInformation
    {
        [JsonPropertyName("followingInfo")]
        public FollowingInfo FollowingInfo { get; set; }

        [JsonPropertyName("distance")]
        public Distance Distance { get; set; }

        [JsonPropertyName("entityUrn")]
        public string EntityUrn { get; set; }

        [JsonPropertyName("following")]
        public bool Following { get; set; }

        [JsonPropertyName("followable")]
        public bool Followable { get; set; }

        [JsonPropertyName("followersCount")]
        public long FollowersCount { get; set; }

        [JsonPropertyName("connectionsCount")]
        public long ConnectionsCount { get; set; }
    }
}