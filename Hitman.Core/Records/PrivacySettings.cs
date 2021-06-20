using System.Text.Json.Serialization;

namespace Hitman.Core.Records
{
    public record PrivacySettings
    {
        [JsonPropertyName("messagingTypingIndicators")]
        public string MessagingTypingIndicators { get; set; }

        [JsonPropertyName("allowOpenProfile")]
        public bool AllowOpenProfile { get; set; }

        [JsonPropertyName("profilePictureVisibilitySetting")]
        public string ProfilePictureVisibilitySetting { get; set; }

        [JsonPropertyName("entityUrn")]
        public string EntityUrn { get; set; }

        [JsonPropertyName("showPublicProfile")]
        public bool ShowPublicProfile { get; set; }

        [JsonPropertyName("showPremiumSubscriberBadge")]
        public bool ShowPremiumSubscriberBadge { get; set; }

        [JsonPropertyName("publicProfilePictureVisibilitySetting")]
        public string PublicProfilePictureVisibilitySetting { get; set; }

        [JsonPropertyName("formerNameVisibilitySetting")]
        public string FormerNameVisibilitySetting { get; set; }

        [JsonPropertyName("messagingSeenReceipts")]
        public string MessagingSeenReceipts { get; set; }

        [JsonPropertyName("allowProfileEditBroadcasts")]
        public bool AllowProfileEditBroadcasts { get; set; }
    }
}