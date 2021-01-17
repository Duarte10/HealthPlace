using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HealthPlace.Core.Models;
using LiteDB;

namespace HealthPlace.Core.Database
{
    /// <summary>
    /// Sets up the database
    /// </summary>
    internal static class Setup
    {
        private static readonly string dbName = @"HealthPlace.db";
        private static readonly DbVersionModel currentVersion = new DbVersionModel() { Id = new Guid("2ffd4bb6-e37b-434c-b016-1edd43aa3c17"), Version = "0.4" };

        /// <summary>
        /// Configures and sets up database for use
        /// </summary>
        internal static void SetupDatabase()
        {
            // If the database is up do date, we don't need to do anything
            if (IsDatabaseUpToDate())
                return;

            // Clear data
            ClearData();

            // Update stored db version
            UpdateCurrentDbVersion();

            // Load default data
            LoadDefaultData();
        }

        /// <summary>
        /// Checks if database is up to date
        /// </summary>
        /// <returns>True if database is up to date</returns>
        private static bool IsDatabaseUpToDate()
        {
            using (var db = new LiteDatabase(dbName))
            {
                var collection = db.GetCollection<DbVersionModel>("DbVersion");
                return collection.FindOne(x => x.Version == currentVersion.Version) != null;
            }

        }

        /// <summary>
        /// Clears database Data
        /// </summary>
        private static void ClearData()
        {
            using (var db = new LiteDatabase(dbName))
            {
                foreach (var type in GetStructuresTypes())
                {
                    var collectionTypeInstance = Activator.CreateInstance(type);
                    db.DropCollection(collectionTypeInstance.ToString());
                }
            }
        }

        /// <summary>
        /// Saves the application current version in the database
        /// </summary>
        private static void UpdateCurrentDbVersion()
        {
            using (var db = new LiteDatabase(dbName))
            {
                var col = db.GetCollection<DbVersionModel>("DbVersion");
                db.GetCollection<DbVersionModel>("DbVersion").Insert(currentVersion);
            }
        }

        /// <summary>
        /// Loads default data into the database
        /// </summary>
        private static void LoadDefaultData()
        {
            using (var db = new LiteDatabase(dbName))
            {
                db.GetCollection<UserModel>(new UserModel().ToString())
                    .InsertBulk(DefaultData.GetDefaultUsers());
            }
        }

        /// <summary>
        /// Get all models types
        /// </summary>
        /// <returns></returns>
        private static Type[] GetStructuresTypes()
        {
            return
              Assembly.GetExecutingAssembly().GetTypes()
                      .Where(t =>
                          String.Equals(
                              t.Namespace, "HealthPlace.Data.Models",
                              StringComparison.Ordinal)
                          && !t.IsAbstract && t.IsClass && t.IsSealed)
                      .ToArray();
        }
    }
}
