using System;

namespace HealthPlace.Core.Models
{
    public class VisitModel : ModelBase
    {
        public VisitorModel Visitor{ get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public override string ToString()
        {
            return "VisitModel";
        }
    }
}
