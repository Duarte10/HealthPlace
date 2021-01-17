using System;
using System.Collections.Generic;

namespace HealthPlace.Logic.Managers
{
    /// <summary>
    /// Interface that defines methods for the Managers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IManager<T>
    {
        #region CRUD Operations

        /// <summary>
        /// Method that inserts a new record and retrieves its Id
        /// </summary>
        /// <param name="record"></param>
        /// <returns>Id of inserted record</returns>
        Guid Insert(T record);

        /// <summary>
        /// Method that updates a record
        /// </summary>
        /// <param name="record">the record</param>
        void Update(T record);

        /// <summary>
        /// Method that deletes a record 
        /// </summary>
        /// <param name="id">The id of the record</param>
        void Delete(Guid id);

        /// <summary>
        /// Method that should retrieve all records in the database
        /// </summary>
        /// <returns>Collection of all records</returns>
        IEnumerable<T> GetAllRecords();

        /// <summary>
        /// Method that retrieves single record by Id
        /// </summary>
        /// <param name="id">Record</param>
        /// <returns></returns>
        T GetRecordById(Guid id);

        #endregion CRUD Operations
    }
}
