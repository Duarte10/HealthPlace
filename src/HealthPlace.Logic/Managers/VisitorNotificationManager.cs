using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthPlace.Core.Database;
using HealthPlace.Core.Models;
using HealthPlace.Logic.Exceptions;
using HealthPlace.Logic.Mappers;
using HealthPlace.Logic.Models;

namespace HealthPlace.Logic.Managers
{
    /// <summary>
    /// Visitor Notification Manager (CRUD operations for Visitor Notification object)
    /// </summary>
    public class VisitorNotificationManager : IManager<VisitorNotification>
    {

        #region CRUD Operations

        /// <summary>
        /// Gets all the visitor notification stored in the database
        /// </summary>
        /// <returns>The collection of visitor notifications</returns>
        public IEnumerable<VisitorNotification> GetAllRecords()
        {
            var visitorNotifications = DbContext<VisitorNotificationModel>.GetAllRecords();
            return visitorNotifications.Select(x => x.ToVisitorNotification()).ToList();
        }

        /// <summary>
        /// Retrieves the visitor notification with the specified Id
        /// </summary>
        /// <param name="id">The Id of the visitor notification</param>
        /// <returns>The visitor notification with the specified id</returns>
        public VisitorNotification GetRecordById(Guid id)
        {
            var visitorNotification = DbContext<VisitorNotificationModel>.GetRecordById(id).ToVisitorNotification();
            if (visitorNotification == null)
                throw new EntityNotFoundException("Visit", "id");
            return visitorNotification;
        }

        /// <summary>
        /// Inserts a new visitor notification
        /// </summary>
        /// <param name="visitorNotification">The visitor notification</param>
        /// <returns>The inserted visitor notification Id</returns>
        public Guid Insert(VisitorNotification visitorNotification)
        {
            this.Validate(visitorNotification);
            return DbContext<VisitorNotificationModel>.Insert(visitorNotification.ToVisitorNotificationModel());
        }

        /// <summary>
        /// Updates a visitor notification record
        /// </summary>
        /// <param name="visitorNotification">The visitor notification record</param>
        public void Update(VisitorNotification visitorNotification)
        {
            this.Validate(visitorNotification);
            DbContext<VisitorNotificationModel>.Update(visitorNotification.ToVisitorNotificationModel());
        }

        /// <summary>
        /// Deletes a visitor notification record
        /// </summary>
        /// <param name="id">The visitor notification record Id</param>
        public void Delete(Guid id)
        {
            // Delete record 
            DbContext<VisitorNotificationModel>.Delete(id);
        }

        #endregion CRUD Operation

        #region Private methods

        /// <summary>
        /// Validates visitor notification
        /// </summary>
        /// <param name="visitorNotification">The visitor notification record</param>
        private void Validate(VisitorNotification visitorNotification)
        {
            if (visitorNotification.Visitor == null)
                throw new EntityValidationException("The visitor notification visitor cannot be null!");
            if (visitorNotification.SentDate == DateTime.MinValue)
                throw new EntityValidationException("The visitor notification sent date is not valid.");
        }

        #endregion Private methods

    }
}
