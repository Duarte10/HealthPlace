using System;
using System.Text.Json.Serialization;

namespace HealthPlace.WebApi.ApiResources
{
    public class PositiveCaseResource
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("visitorId")]
        public Guid VisitorId { get; set; }

        [JsonPropertyName("visitDate")]
        public DateTime VisitDate { get; set; }

        [JsonPropertyName("allUsersNotified")]
        public bool AllUsersNotified { get; set; }
    }
}
