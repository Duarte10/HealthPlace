using HealthPlace.Logic.Models;
using HealthPlace.WebApi.ApiResources;

namespace HealthPlace.WebApi.Resources.Mappers
{
    /// <summary>
    /// Maps a Visitor to a VisitorResource and vice-versa
    /// </summary>
    internal static class VisitorMapper
    {
        /// <summary>
        /// Maps a Visitor to a VisitorResource.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <returns>A visitor</returns>
        public static VisitorResource ToVisitorResource(this Visitor visitor)
        {
            return new VisitorResource()
            {
                Id = visitor.Id,
                Email = visitor.Email,
                Mobile = visitor.Mobile,
                Name = visitor.Name
            };
        }

        /// <summary>
        /// Maps a Visitor to VisitorResource.
        /// </summary>
        /// <param name="visitorResource">The visitor.</param>
        /// <returns>A Visitor</returns>
        public static Visitor ToVisitor(this VisitorResource visitorResource)
        {
            return new Visitor()
            {
                Id = visitorResource.Id,
                Email = visitorResource.Email,
                Mobile = visitorResource.Mobile,
                Name = visitorResource.Name
            };
        }
    }
}
