using System;
using System.Text.Json.Serialization;

namespace HealthPlace.WebApi.ApiResources
{
    public class NewUserResource
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonIgnore]
        public string CreatedBy { get; set; }

    }
}
