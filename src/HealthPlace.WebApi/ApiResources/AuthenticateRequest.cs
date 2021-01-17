using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HealthPlace.WebApi.ApiResources
{
    public class AuthenticateRequest
    {
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
