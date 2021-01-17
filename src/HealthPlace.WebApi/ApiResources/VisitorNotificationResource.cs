using System;
using System.Text.Json.Serialization;

namespace HealthPlace.WebApi.ApiResources
{
    public class VisitorNotificationResource
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("visitorId")]
        public Guid VisitorId { get; set; }

        [JsonPropertyName("positiveCaseId")]
        public Guid PositiveCaseId { get; set; }

        [JsonPropertyName("sentDate")]
        public DateTime SentDate { get; set; }
    }
}
