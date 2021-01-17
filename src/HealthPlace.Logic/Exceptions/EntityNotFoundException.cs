using System;

namespace HealthPlace.Logic.Exceptions
{

    /// <summary>
    /// Exception that should be thrown when an entity with the specified filter was not found
    /// </summary>
    public class EntityNotFoundException : ApplicationException
    {
        /// <summary>
        /// Throws an EntityNotFoundException
        /// </summary>
        public EntityNotFoundException() : base("An entity with the specified filter wasn't found")
        {
        }

        /// <summary>
        /// Throws an EntityNotFoundException
        /// </summary>
        /// <param name="entityName">The entity name</param>
        /// <param name="filterByFieldName">The name of the field the entity was filtered by</param>
        public EntityNotFoundException(string entityName, string filterByFieldName)
            : base($"A {entityName} wasn't found with the specified {filterByFieldName}") { }

        /// <summary>
        /// Throws an EntityNotFoundException
        /// </summary>
        /// <param name="s">Custom message</param>
        /// <param name="e">The exception</param>
        public EntityNotFoundException(string s, Exception e)
        {
            throw new Exception($"{e.Message} - The entity with the specified filter wasn't found : {s}");
        }

    }
}
