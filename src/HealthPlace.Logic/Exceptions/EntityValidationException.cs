using System;

namespace HealthPlace.Logic.Exceptions
{
    /// <summary>
    /// Exception that should be thrown when an entity has invalid fields
    /// </summary>
    public class EntityValidationException : ApplicationException
    {
        /// <summary>
        /// Throws a new EntityValidationException
        /// </summary>
        public EntityValidationException() : base("The entity has invalid fields")
        {
        }

        /// <summary>
        /// Throws a new EntityValidationException
        /// </summary>
        /// <param name="s">Custom message</param>
        public EntityValidationException(string s) : base($"The entity has invalid fields : {s}") { }

        /// <summary>
        /// EntityValidationException
        /// </summary>
        /// <param name="s">Custom message</param>
        /// <param name="e">The exception</param>
        public EntityValidationException(string s, Exception e)
        {
            throw new Exception($"{e.Message} - The entity has invalid fields : {s}");
        }
    }
}
