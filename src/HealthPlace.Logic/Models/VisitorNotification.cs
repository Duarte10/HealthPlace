using System;

namespace HealthPlace.Logic.Models
{
    public class VisitorNotification
    {
        #region Public Properties

        public Guid Id { get; set; }

        public Visitor Visitor { get; set; }

        public PositiveCase PositiveCase { get; set; }

        public DateTime SentDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        #endregion Public Properties
    }
}
