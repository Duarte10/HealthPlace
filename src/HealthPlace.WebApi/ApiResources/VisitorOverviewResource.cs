using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthPlace.WebApi.ApiResources
{
    public class VisitorOverviewResource
    {
        #region Constructors

        public VisitorOverviewResource(VisitorResource visitor)
        {
            Id = visitor.Id;
            Name = visitor.Name;
            Email = visitor.Email;
            Mobile = visitor.Mobile;
            Visits = new List<VisitResource>();
            Notifications = new List<VisitorNotificationResource>();
            PositiveCases = new List<PositiveCaseResource>();
        }

        #endregion 

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("visits")]
        public IEnumerable<VisitResource> Visits { get; set; }

        [JsonPropertyName("notifications")]
        public IEnumerable<VisitorNotificationResource> Notifications { get; set; }

        [JsonPropertyName("positiveCases")]
        public IEnumerable<PositiveCaseResource> PositiveCases { get; set; }

    }
}
