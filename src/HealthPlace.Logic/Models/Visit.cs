using System;

namespace HealthPlace.Logic.Models
{
    public class Visit
    {
        #region Public Properties

        public Guid Id { get; set; }

        public Visitor Visitor { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        #endregion Public Properties  
    }
}
