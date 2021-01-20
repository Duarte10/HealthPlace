using System;
using System.Collections.Generic;
using System.Linq;
using HealthPlace.Core.Database;
using HealthPlace.Core.Models;
using HealthPlace.Logic.Exceptions;
using HealthPlace.Logic.Mappers;
using HealthPlace.Logic.Models;

namespace HealthPlace.Logic.Managers
{
    /// <summary>
    /// Visit Manager (CRUD operations for Visit object)
    /// </summary>
    public class VisitManager : IManager<Visit>
    {

        #region CRUD Operations

        /// <summary>
        /// Gets all the Visit's stored in the database
        /// </summary>
        /// <returns>The collection of Visit's</returns>
        public IEnumerable<Visit> GetAllRecords()
        {
            var visits = DbContext<VisitModel>.GetAllRecords();
            return visits.Select(x => x.ToVisit()).ToList();
        }

        /// <summary>
        /// Gets the visits with the specified user id.
        /// </summary>
        /// <param name="visitorId">The id of the user.</param>
        /// <returns>The collection of visits with the specified user id.</returns>
        public IEnumerable<Visit> GetRecordsByVisitorId(Guid visitorId)
        {
            var visits = DbContext<VisitModel>.GetAllRecords().Where(v => v.Visitor.Id == visitorId);
            return visits.Select(x => x.ToVisit()).ToList();
        }

        /// <summary>
        /// Retrieves the Visit with the specified Id
        /// </summary>
        /// <param name="id">The Id of the Visit</param>
        /// <returns>Visit with the specified id</returns>
        public Visit GetRecordById(Guid id)
        {
            var visit = DbContext<VisitModel>.GetRecordById(id).ToVisit();
            if (visit == null)
                throw new EntityNotFoundException("Visit", "id");
            return visit;
        }

        /// <summary>
        /// Inserts a new Visit
        /// </summary>
        /// <param name="visit">The Visit</param>
        /// <returns>The inserted Visit Id</returns>
        public Guid Insert(Visit visit)
        {
            this.Validate(visit);
            return DbContext<VisitModel>.Insert(visit.ToVisitModel());
        }

        /// <summary>
        /// Updates a Visit record
        /// </summary>
        /// <param name="visit">The Visit record</param>
        public void Update(Visit visit)
        {
            this.Validate(visit);
            DbContext<VisitModel>.Update(visit.ToVisitModel());
        }

        /// <summary>
        /// Deletes a Visit record
        /// </summary>
        /// <param name="id">The Visit record Id</param>
        public void Delete(Guid id)
        {
            Visit visit = GetRecordById(id);

            // Delete record 
            DbContext<VisitModel>.Delete(id);
        }

        #endregion CRUD Operation

        #region Private methods

        /// <summary>
        /// Validates Visit
        /// </summary>
        /// <param name="visit">The visit record</param>
        private void Validate(Visit visit)
        {
            if (visit.Visitor == null)
                throw new EntityValidationException("Visit visitor cannot be null!");
            if (visit.CheckIn == DateTime.MinValue)
                throw new EntityValidationException("Visit check-in date is not valid.");
        }

        #endregion Private methods

    }
}
