using HealthPlace.Core.Models;
using HealthPlace.Logic.Models;

namespace HealthPlace.Logic.Mappers
{
    /// <summary>
    /// Maps a Visit to a VisitModel and vice-versa
    /// </summary>
    internal static class VisitMapper
    {
        /// <summary>
        /// Maps a VisitModel to Visit.
        /// </summary>
        /// <param name="visitModel">The visit model.</param>
        /// <returns>A visit</returns>
        public static Visit ToVisit(this VisitModel visitModel)
        {
            return new Visit()
            {
                Id = visitModel.Id,
                CreatedBy = visitModel.CreatedBy,
                CreatedOn = visitModel.CreatedOn,
                UpdatedBy = visitModel.UpdatedBy,
                UpdatedOn = visitModel.UpdatedOn,
                CheckIn = visitModel.CheckIn,
                CheckOut = visitModel.CheckOut,
                Visitor = visitModel.Visitor?.ToVisitor()
            };
        }

        /// <summary>
        /// Maps a VisitModel to Visit.
        /// </summary>
        /// <param name="visit">The visit model.</param>
        /// <returns>A VisitModel</returns>
        public static VisitModel ToVisitModel(this Visit visit)
        {
            return new VisitModel()
            {
                Id = visit.Id,
                CreatedBy = visit.CreatedBy,
                CreatedOn = visit.CreatedOn,
                UpdatedBy = visit.UpdatedBy,
                UpdatedOn = visit.UpdatedOn,
                CheckIn = visit.CheckIn,
                CheckOut = visit.CheckOut,
                Visitor = visit.Visitor?.ToVisitorModel()
            };
        }
    }
}
