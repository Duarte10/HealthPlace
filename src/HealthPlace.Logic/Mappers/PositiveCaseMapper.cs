using HealthPlace.Core.Models;
using HealthPlace.Logic.Models;

namespace HealthPlace.Logic.Mappers
{
    /// <summary>
    /// Maps a PositiveCase to a PositiveCaseModel and vice-versa
    /// </summary>
    internal static class PositiveCaseMapper
    {
        /// <summary>
        /// Maps a PositiveCaseModel to PositiveCase.
        /// </summary>
        /// <param name="positiveCaseModel">The positive case model.</param>
        /// <returns>A positive case</returns>
        public static PositiveCase ToPositiveCase(this PositiveCaseModel positiveCaseModel)
        {
            return new PositiveCase()
            {
                Id = positiveCaseModel.Id,
                CreatedBy = positiveCaseModel.CreatedBy,
                CreatedOn = positiveCaseModel.CreatedOn,
                UpdatedBy = positiveCaseModel.UpdatedBy,
                UpdatedOn = positiveCaseModel.UpdatedOn,
                Visitor = positiveCaseModel.Visitor?.ToVisitor(),
                AllUsersNotified  = positiveCaseModel.AllUsersNotified,
                VisitDate = positiveCaseModel.VisitDate
            };
        }

        /// <summary>
        /// Maps a PositiveCase to PositiveCaseModel.
        /// </summary>
        /// <param name="positiveCase">The positive case.</param>
        /// <returns>A positive case model</returns>
        public static PositiveCaseModel ToPositiveCaseModel(this PositiveCase positiveCase)
        {
            return new PositiveCaseModel()
            {
                Id = positiveCase.Id,
                CreatedBy = positiveCase.CreatedBy,
                CreatedOn = positiveCase.CreatedOn,
                UpdatedBy = positiveCase.UpdatedBy,
                UpdatedOn = positiveCase.UpdatedOn,
                Visitor = positiveCase.Visitor?.ToVisitorModel(),
                AllUsersNotified = positiveCase.AllUsersNotified,
                VisitDate = positiveCase.VisitDate
            };
        }
    }
}
