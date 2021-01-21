using HealthPlace.Logic.Models;
using HealthPlace.WebApi.ApiResources;

namespace HealthPlace.WebApi.Resources.Mappers
{
    /// <summary>
    /// Maps a Visit to a VisitResource and vice-versa
    /// </summary>
    internal static class VisitMapper
    {
        /// <summary>
        /// Maps a Visit to a VisitResource.
        /// </summary>
        /// <param name="visit">The visitor notification.</param>
        /// <returns>A visitor notification</returns>
        public static VisitResource ToVisitResource(this Visit visit)
        {
            return new VisitResource()
            {
                Id = visit.Id,
                CheckIn = visit.CheckIn,
                CheckOut = visit.CheckOut,
                VisitorId = visit.Visitor.Id
            };
        }

        /// <summary>
        /// Maps a Visit to VisitResource.
        /// </summary>
        /// <param name="visitResource">The visitor notification.</param>
        /// <returns>A Visit</returns>
        public static Visit ToVisit(this VisitResource visitResource)
        {
            return new Visit()
            {
                Id = visitResource.Id,
                CheckIn = visitResource.CheckIn,
                CheckOut = visitResource.CheckOut,
                Visitor = new Visitor()
                {
                    Id = visitResource.VisitorId
                }
            };
        }
    }
}
