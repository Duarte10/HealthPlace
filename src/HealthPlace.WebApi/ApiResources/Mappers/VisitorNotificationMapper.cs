using HealthPlace.Logic.Models;
using HealthPlace.WebApi.ApiResources;

namespace HealthPlace.WebApi.Resources.Mappers
{
    /// <summary>
    /// Maps a VisitorNotification to a VisitorNotificationResource and vice-versa
    /// </summary>
    internal static class VisitorNotificationMapper
    {
        /// <summary>
        /// Maps a VisitorNotification to a VisitorNotificationResource.
        /// </summary>
        /// <param name="visitorNotification">The visitor notification.</param>
        /// <returns>A visitor notification</returns>
        public static VisitorNotificationResource ToVisitorNotificationResource(this VisitorNotification visitorNotification)
        {
            return new VisitorNotificationResource()
            {
                Id = visitorNotification.Id,
                PositiveCaseId = visitorNotification.PositiveCase.Id,
                VisitorId = visitorNotification.Visitor.Id,
                SentDate = visitorNotification.SentDate
            };
        }

        /// <summary>
        /// Maps a VisitorNotification to VisitorNotificationResource.
        /// </summary>
        /// <param name="visitorNotificationResource">The visitor notification.</param>
        /// <returns>A VisitorNotification</returns>
        public static VisitorNotification ToVisitorNotification(this VisitorNotificationResource visitorNotificationResource)
        {
            return new VisitorNotification()
            {
                Id = visitorNotificationResource.Id,
                PositiveCase = new PositiveCase()
                {
                    Id = visitorNotificationResource.PositiveCaseId
                },

                Visitor = new Visitor()
                {
                    Id = visitorNotificationResource.VisitorId
                },
                SentDate = visitorNotificationResource.SentDate
            };
        }
    }
}
