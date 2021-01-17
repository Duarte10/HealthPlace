using System;

namespace HealthPlace.Logic.Models
{
    public class User
    {
        #region Public Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        #endregion Public Properties
    }
}
