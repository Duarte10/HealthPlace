using HealthPlace.Core.Models;
using HealthPlace.Logic.Models;

namespace HealthPlace.Logic.Mappers
{
    /// <summary>
    /// Maps a Visitor to a VisitorModel and vice-versa
    /// </summary>
    internal static class VisitorMapper
    {
        /// <summary>
        /// Maps a VisitorModel to Visitor.
        /// </summary>
        /// <param name="visitorModel">The visitor model.</param>
        /// <returns>A visitor</returns>
        public static Visitor ToVisitor(this VisitorModel visitorModel)
        {
            return new Visitor()
            {
                Id = visitorModel.Id,
                CreatedBy = visitorModel.CreatedBy,
                CreatedOn = visitorModel.CreatedOn,
                UpdatedBy = visitorModel.UpdatedBy,
                UpdatedOn = visitorModel.UpdatedOn,
                Mobile = visitorModel.Mobile,
                Email = visitorModel.Email,
                Name = visitorModel.Name
            };
        }

        /// <summary>
        /// Maps a Visitor to VisitorModel.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        /// <returns>A VisitorModel</returns>
        public static VisitorModel ToVisitorModel(this Visitor visitor)
        {
            return new VisitorModel()
            {
                Id = visitor.Id,
                CreatedBy = visitor.CreatedBy,
                CreatedOn = visitor.CreatedOn,
                UpdatedBy = visitor.UpdatedBy,
                UpdatedOn = visitor.UpdatedOn,
                Mobile = visitor.Mobile,
                Email = visitor.Email,
                Name = visitor.Name
            };
        }
    }
}
