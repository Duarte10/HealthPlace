using HealthPlace.Core.Models;
using HealthPlace.Logic.Models;

namespace HealthPlace.Logic.Mappers
{
    /// <summary>
    /// Maps a VisitorNotification to a VisitorNotificationModel and vice-versa
    /// </summary>
    internal static class VisitorNotificationMapper
    {
        /// <summary>
        /// Maps a VisitorNotificationModel to VisitorNotification.
        /// </summary>
        /// <param name="visitorNotificationModel">The visitor notification model.</param>
        /// <returns>A visitor notification</returns>
        public static VisitorNotification ToVisitorNotification(this VisitorNotificationModel visitorNotificationModel)
        {
            return new VisitorNotification()
            {
                Id = visitorNotificationModel.Id,
                CreatedBy = visitorNotificationModel.CreatedBy,
                CreatedOn = visitorNotificationModel.CreatedOn,
                UpdatedBy = visitorNotificationModel.UpdatedBy,
                UpdatedOn = visitorNotificationModel.UpdatedOn,
                Visitor = visitorNotificationModel.Visitor?.ToVisitor(),
                PositiveCase = visitorNotificationModel.PositiveCase?.ToPositiveCase(),
                SentDate = visitorNotificationModel.SentDate
            };
        }

        /// <summary>
        /// Maps a VisitorNotification to VisitorNotificationModel.
        /// </summary>
        /// <param name="visitorNotification">The visitor notification model.</param>
        /// <returns>A visitor notification model</returns>
        public static VisitorNotificationModel ToVisitorNotificationModel(this VisitorNotification visitorNotification)
        {
            return new VisitorNotificationModel()
            {
                Id = visitorNotification.Id,
                CreatedBy = visitorNotification.CreatedBy,
                CreatedOn = visitorNotification.CreatedOn,
                UpdatedBy = visitorNotification.UpdatedBy,
                UpdatedOn = visitorNotification.UpdatedOn,
                Visitor = visitorNotification.Visitor?.ToVisitorModel(),
                PositiveCase = visitorNotification.PositiveCase?.ToPositiveCaseModel(),
                SentDate = visitorNotification.SentDate
            };
        }
    }
}
