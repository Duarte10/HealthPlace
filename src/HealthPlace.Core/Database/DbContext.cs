using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthPlace.Core.Models;
using LiteDB;

namespace HealthPlace.Core.Database
{

    /// <summary>
    /// Db Context - Handles CRUD Operation for generic Db objects that implement DataBaseStructure and IDefaultDbEntity interface
    /// </summary>
    /// <typeparam name="T">DbResource object</typeparam>
    public static class DbContext<T> where T : ModelBase
    {
        private static readonly string dbName = @"HealthPlace.db";

        #region CRUD Operations

        /// <summary>
        /// Retrieves all records
        /// </summary>
        /// <returns>All records</returns>
        public static IEnumerable<T> GetAllRecords()
        {
            using (var db = new LiteDatabase(dbName))
            {
                string collectioName = GetCollectionName();
                var collection = db.GetCollection<T>(collectioName);
                var result = collection.FindAll().ToList();
                return result;
            }
        }

        /// <summary>
        /// Gets record by id
        /// </summary>
        /// <param name="id">Record Id</param>
        /// <returns>Record</returns>
        public static T GetRecordById(Guid id)
        {
            using (var db = new LiteDatabase(dbName))
            {
                string collectionName = GetCollectionName();
                var collection = db.GetCollection<T>(collectionName);
                return collection.FindById(id);
            }
        }

        /// <summary>
        /// Inserts record in the database
        /// </summary>
        /// <param name="record">The record</param>
        /// <returns>The id of the inserted record</returns>
        public static Guid Insert(T record)
        {
            record.Id = Guid.NewGuid();
            record.CreatedOn = DateTime.UtcNow;
            record.UpdatedOn = record.CreatedOn;
            record.UpdatedBy = record.CreatedBy;
            using (var db = new LiteDatabase(dbName))
            {
                string collectionName = GetCollectionName();
                var collection = db.GetCollection<T>(collectionName);
                collection.Insert(record);
            }

            return record.Id;
        }

        /// <summary>
        /// Updates record in the database
        /// </summary>
        /// <param name="record">The record</param>
        public static void Update(T record)
        {
            using (var db = new LiteDatabase(dbName))
            {
                string collectionName = GetCollectionName();
                var collection = db.GetCollection<T>(collectionName);
                record.UpdatedOn = DateTime.UtcNow;
                collection.Update(record);
            }
        }

        /// <summary>
        /// Deletes record in the database
        /// </summary>
        /// <param name="id">The id of the record</param>
        public static void Delete(Guid id)
        {
            using (var db = new LiteDatabase(dbName))
            {
                string collectionName = GetCollectionName();
                var collection = db.GetCollection<T>(collectionName);
                collection.Delete(id);
            }
        }

        #endregion CRUD Operations

        #region Private Methods

        /// <summary>
        /// Gets the collection name based on the current DbResource
        /// </summary>
        /// <returns>Collection name</returns>
        private static string GetCollectionName()
        {
            var collectionTypeInstance = Activator.CreateInstance(typeof(T));
            return collectionTypeInstance.ToString();
        }

        #endregion Private Methods
    }
    

}
