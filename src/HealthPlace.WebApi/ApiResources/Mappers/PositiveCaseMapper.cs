using HealthPlace.Core.Models;
using HealthPlace.Logic.Models;
using HealthPlace.WebApi.ApiResources;

namespace HealthPlace.WebApi.Resources.Mappers
{
    /// <summary>
    /// Maps a PositiveCase to a PositiveCaseResource and vice-versa
    /// </summary>
    internal static class PositiveCaseMapper
    {
        /// <summary>
        /// Maps a PositiveCase to a PositiveCaseResource.
        /// </summary>
        /// <param name="positiveCase">The positive case.</param>
        /// <returns>A positive case</returns>
        public static PositiveCaseResource ToPositiveCaseResource(this PositiveCase positiveCase)
        {
            return new PositiveCaseResource()
            {
                Id = positiveCase.Id,
                AllUsersNotified = positiveCase.AllUsersNotified,
                VisitDate = positiveCase.VisitDate,
                VisitorId = positiveCase.Visitor.Id
            };
        }

        /// <summary>
        /// Maps a PositiveCase to PositiveCaseResource.
        /// </summary>
        /// <param name="positiveCaseResource">The positive case.</param>
        /// <returns>A PositiveCase</returns>
        public static PositiveCase ToPositiveCase(this PositiveCaseResource positiveCaseResource)
        {
            return new PositiveCase()
            {
                Id = positiveCaseResource.Id,
                AllUsersNotified = positiveCaseResource.AllUsersNotified,
                VisitDate = positiveCaseResource.VisitDate,
                Visitor = new Visitor()
                {
                    Id = positiveCaseResource.VisitorId
                }
            };
        }
    }
}
