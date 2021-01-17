using System;
using System.Text.Json.Serialization;

namespace HealthPlace.WebApi.ApiResources
{
    public class VisitorResource
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

    }
}
