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
    /// PositiveCase Manager (CRUD operations for PositiveCase object)
    /// </summary>
    public class PositiveCaseManager : IManager<PositiveCase>
    {

        #region CRUD Operations

        /// <summary>
        /// Gets all the positive cases stored in the database
        /// </summary>
        /// <returns>The collection of positive cases</returns>
        public IEnumerable<PositiveCase> GetAllRecords()
        {
            var positiveCases = DbContext<PositiveCaseModel>.GetAllRecords();
            return positiveCases.Select(x => x.ToPositiveCase()).ToList();
        }

        /// <summary>
        /// Gets the positive cases with the specified user id.
        /// </summary>
        /// <param name="visitorId">The id of the user.</param>
        /// <returns>The collection of positive cases with the specified user id.</returns>
        public IEnumerable<PositiveCase> GetRecordsByVisitorId(Guid visitorId)
        {
            var positiveCases = DbContext<PositiveCaseModel>.GetAllRecords().Where(v => v.Visitor.Id == visitorId);
            return positiveCases.Select(x => x.ToPositiveCase()).ToList();
        }

        /// <summary>
        /// Retrieves the visitor with the specified Id
        /// </summary>
        /// <param name="id">The Id of the visitor</param>
        /// <returns>The visitor with the specified id</returns>
        public PositiveCase GetRecordById(Guid id)
        {
            var positiveCase = DbContext<PositiveCaseModel>.GetRecordById(id).ToPositiveCase();
            if (positiveCase == null)
                throw new EntityNotFoundException("PositiveCase", "id");
            return positiveCase;
        }

        /// <summary>
        /// Inserts a new positive case
        /// </summary>
        /// <param name="positiveCase">The positive case</param>
        /// <returns>The inserted positive case Id</returns>
        public Guid Insert(PositiveCase positiveCase)
        {
            this.Validate(positiveCase);
            return DbContext<PositiveCaseModel>.Insert(positiveCase.ToPositiveCaseModel());
        }

        /// <summary>
        /// Updates a positve case record
        /// </summary>
        /// <param name="positiveCase">The positive case record</param>
        public void Update(PositiveCase positiveCase)
        {
            this.Validate(positiveCase);
            DbContext<PositiveCaseModel>.Update(positiveCase.ToPositiveCaseModel());
        }

        /// <summary>
        /// Deletes a positive case record
        /// </summary>
        /// <param name="id">The positive case record Id</param>
        public void Delete(Guid id)
        {
            // Delete record 
            DbContext<PositiveCaseModel>.Delete(id);
        }

        #endregion CRUD Operation

        #region Private methods

        /// <summary>
        /// Validates a positive case
        /// </summary>
        /// <param name="positiveCase">The positive case record</param>
        private void Validate(PositiveCase positiveCase)
        {
            if (positiveCase.Visitor == null)
                throw new EntityValidationException("The positve case visitor cannot be null!");

            if (positiveCase.VisitDate == DateTime.MinValue)
                throw new EntityValidationException("The positve case visit date is not valid.");

        }

        #endregion Private methods

    }
}
