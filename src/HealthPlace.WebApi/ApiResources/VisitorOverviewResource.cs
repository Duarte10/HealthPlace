using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("visits")]
        public IEnumerable<VisitResource> Visits { get; set; }

        [JsonProperty("notifications")]
        public IEnumerable<VisitorNotificationResource> Notifications { get; set; }

        [JsonProperty("positiveCases")]
        public IEnumerable<PositiveCaseResource> PositiveCases { get; set; }

    }
}
