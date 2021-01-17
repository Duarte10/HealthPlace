using System;
using System.Text.Json.Serialization;

namespace HealthPlace.WebApi.ApiResources
{
    public class VisitResource
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("visitorId")]
        public Guid VisitorId { get; set; }

        [JsonPropertyName("checkIn")]
        public DateTime CheckIn { get; set; }

        [JsonPropertyName("checkOut")]
        public DateTime? CheckOut { get; set; }
    }
}
