namespace HealthPlace.Core.Models
{
    public class VisitorModel : ModelBase
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public override string ToString()
        {
            return "VisitorModel";
        }
    }
}
