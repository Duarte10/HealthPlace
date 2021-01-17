using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthPlace.Logic.Exceptions
{
    /// <summary>
    /// Exception that should be thrown when trying to delete an entity that has related records in the database
    /// </summary>
    public class EntityHasRelatedRecordsException : ApplicationException
    {
        /// <summary>
        /// Throws an EntityHasRelatedRecordsException
        /// </summary>
        public EntityHasRelatedRecordsException() : base("Cannot delete this entity, it has related records")
        {
        }

        /// <summary>
        /// Throws an EntityHasRelatedRecordsException
        /// </summary>
        /// <param name="entityName">The name of the entity that has related records</param>
        public EntityHasRelatedRecordsException(string entityName) : base($"Cannot delete entity {entityName}, it has related records") { }

        /// <summary>
        /// Throws an EntityHasRelatedRecordsException
        /// </summary>
        /// <param name="entityName">The name of the entity that has related records</param>
        /// <param name="foreignEntityName">The name of the entity of the related records</param>
        public EntityHasRelatedRecordsException(string entityName, string foreignEntityName) :
            base($"Cannot delete entity {entityName}, it has {foreignEntityName}(s) associated")
        { }

        /// <summary>
        /// Throws an EntityHasRelatedRecordsException
        /// </summary>
        /// <param name="entityName">The name of the entity that has related records</param>
        /// <param name="e">The exception</param>
        public EntityHasRelatedRecordsException(string entityName, Exception e)
        {
            throw new Exception($"{e.Message} - Cannot delete entity {entityName}, it has related records");
        }
    }
}
