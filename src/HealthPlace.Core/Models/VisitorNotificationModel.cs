using System;

namespace HealthPlace.Core.Models
{
    public class VisitorNotificationModel : ModelBase
    {
        public VisitorModel Visitor { get; set; }

        public PositiveCaseModel PositiveCase { get; set; }

        public DateTime SentDate { get; set; }

        public override string ToString()
        {
            return "VisitorNotificationModel";
        }

    }
}
