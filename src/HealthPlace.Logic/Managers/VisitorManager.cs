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
    /// Visitor Manager (CRUD operations for Visitor object)
    /// </summary>
    public class VisitorManager : IManager<Visitor>
    {

        #region CRUD Operations

        /// <summary>
        /// Gets all the visitors stored in the database
        /// </summary>
        /// <returns>The collection of visitors</returns>
        public IEnumerable<Visitor> GetAllRecords()
        {
            var visitors = DbContext<VisitorModel>.GetAllRecords();
            return visitors.Select(x => x.ToVisitor()).ToList();
        }

        /// <summary>
        /// Retrieves the visitor with the specified Id
        /// </summary>
        /// <param name="id">The Id of the visitor</param>
        /// <returns>The visitor with the specified id</returns>
        public Visitor GetRecordById(Guid id)
        {
            var visitor = DbContext<VisitorModel>.GetRecordById(id).ToVisitor();
            if (visitor == null)
                throw new EntityNotFoundException("Visitor", "id");
            return visitor;
        }

        /// <summary>
        /// Retrieves the visitor with the specified email
        /// </summary>
        /// <param name="email">The email of the visitor</param>
        /// <returns>The visitor with the specified email</returns>
        public Visitor GetRecordByEmail(string email)
        {
            var visitor = DbContext<VisitorModel>.GetAllRecords().Where(v => !string.IsNullOrEmpty(v.Email) && v.Email.ToUpper() == email.ToUpper()).FirstOrDefault();
            if (visitor != null)
            {
                return visitor.ToVisitor();
            }
            return null;
        }

        /// <summary>
        /// Retrieves the visitor with the specified mobile
        /// </summary>
        /// <param name="mobile">The email of the mobile</param>
        /// <returns>The visitor with the specified mobile number</returns>
        public Visitor GetRecordByMobile(string mobile)
        {
            var visitor = DbContext<VisitorModel>.GetAllRecords().Where(v => !string.IsNullOrEmpty(v.Mobile) && v.Mobile.ToUpper() == mobile.ToUpper()).FirstOrDefault();
            if (visitor != null)
            {
                return visitor.ToVisitor();
            }
            return null;
        }


        /// <summary>
        /// Inserts a new visitor
        /// </summary>
        /// <param name="visitor">The visitor</param>
        /// <returns>The inserted visitor Id</returns>
        public Guid Insert(Visitor visitor)
        {
            this.Validate(visitor);
            return DbContext<VisitorModel>.Insert(visitor.ToVisitorModel());
        }

        /// <summary>
        /// Updates a visitor record
        /// </summary>
        /// <param name="visitor">The visitor record</param>
        public void Update(Visitor visitor)
        {
            this.Validate(visitor);
            DbContext<VisitorModel>.Update(visitor.ToVisitorModel());
        }

        /// <summary>
        /// Deletes a visitor record
        /// </summary>
        /// <param name="id">The visitor record Id</param>
        public void Delete(Guid id)
        {
            Visitor visitor = this.GetRecordById(id);

            // Validate
            this.OnDelete(visitor);

            // Delete record 
            DbContext<VisitorModel>.Delete(id);
        }

        #endregion CRUD Operation

        #region Private methods

        /// <summary>
        /// Validates visitor
        /// </summary>
        /// <param name="visitor">The visitor record</param>
        private void Validate(Visitor visitor)
        {
            if (string.IsNullOrEmpty(visitor.Name))
                throw new EntityValidationException("The visitor name cannot be empty!");

            if (string.IsNullOrEmpty(visitor.Mobile) && string.IsNullOrEmpty(visitor.Email))
                throw new EntityValidationException("The visitor must have at least an Email or Mobile number filled.");
            
            
            if (!string.IsNullOrEmpty(visitor.Email))
            {
                Visitor visitorWithSameEmail = this.GetRecordByEmail(visitor.Email);
                if (visitorWithSameEmail != null && visitorWithSameEmail.Id != visitor.Id)
                {
                    throw new EntityValidationException($"There is already a visitor created with the email {visitor.Email}");
                }
            }

            if (!string.IsNullOrEmpty(visitor.Mobile))
            {
                Visitor visitorWithSameMobile = this.GetRecordByMobile(visitor.Mobile);
                if (visitorWithSameMobile != null && visitorWithSameMobile.Id != visitor.Id)
                {
                    throw new EntityValidationException($"There is already a visitor created with the mobile {visitor.Mobile}");
                }
            }
        }

        /// <summary>
        /// Validates the Visitor record to delete
        /// Checks if it has for related records
        /// Throws an exception if the Visitor has related records
        /// </summary>
        /// <param name="visitor">The Visitor to delete</param>
        private void OnDelete(Visitor visitor)
        {            
            VisitManager visitMng = new VisitManager();
            if (visitMng.GetAllRecords().Any(x => x.Visitor?.Id == visitor.Id))
                throw new EntityHasRelatedRecordsException("Vistor", "Visit");

            VisitorNotificationManager visitorNtMng = new VisitorNotificationManager();
            if (visitorNtMng.GetAllRecords().Any(x => x.Visitor?.Id == visitor.Id))
                throw new EntityHasRelatedRecordsException("Vistor", "Visitor Notification");

            PositiveCaseManager positiveCaseMng = new PositiveCaseManager();
            if (positiveCaseMng.GetAllRecords().Any(x => x.Visitor?.Id == visitor.Id))
                throw new EntityHasRelatedRecordsException("Vistor", "Positve Case");
        }

        #endregion Private methods

    }
}
