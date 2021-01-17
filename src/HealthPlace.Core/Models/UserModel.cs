namespace HealthPlace.Core.Models
{
    public class UserModel : ModelBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }

        public override string ToString()
        {
            return "UserModel";
        }
    }
}
