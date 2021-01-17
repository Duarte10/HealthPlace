using System;

namespace HealthPlace.Core.Models
{
    public class PositiveCaseModel : ModelBase
    {
        public VisitorModel Visitor { get; set; }

        public DateTime VisitDate { get; set; }

        public bool AllUsersNotified { get; set; }

        public override string ToString()
        {
            return "PositiveCaseModel";
        }
    }
}
