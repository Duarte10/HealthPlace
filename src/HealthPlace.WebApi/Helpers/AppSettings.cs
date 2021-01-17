namespace HealthPlace.WebApi.Helpers
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }

        public int JwtDurationHours { get; set; }
    }
}
