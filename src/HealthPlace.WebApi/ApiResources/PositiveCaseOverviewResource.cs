using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HealthPlace.WebApi.ApiResources
{
    public class PositiveCaseOverviewResource
    {
        public PositiveCaseOverviewResource(PositiveCaseResource positiveCase)
        {
            Id = positiveCase.Id;
            VisitDate = positiveCase.VisitDate;
            VisitorId = positiveCase.VisitorId;
            AllUsersNotified = positiveCase.AllUsersNotified;
            CollidingVisits = new List<AffectedVisitsResource>();
        }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("visitorId")]
        public Guid VisitorId { get; set; }

        [JsonPropertyName("visitorName")]
        public string VisitorName { get; set; }

        [JsonPropertyName("visitDate")]
        public DateTime VisitDate { get; set; }

        [JsonPropertyName("allUsersNotified")]
        public bool AllUsersNotified { get; set; }

        [JsonPropertyName("collidingVisits")]
        public IList<AffectedVisitsResource> CollidingVisits { get; set; }
    }

    public class AffectedVisitsResource
    {
        [JsonPropertyName("visitId")]
        public Guid VisitId { get; set; }

        [JsonPropertyName("visitDate")]
        public DateTime VisitDate { get; set; }

        [JsonPropertyName("visitorId")]
        public Guid VisitorId { get; set; }

        [JsonPropertyName("visitorName")]
        public string VisitorName { get; set; }

        [JsonPropertyName("notificationSent")]
        public bool NotificationSent { get; set; }
    }
}
