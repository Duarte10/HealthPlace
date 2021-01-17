using HealthPlace.Core.Database;

namespace HealthPlace.Core
{
    public static class Startup
    {
        /// <summary>
        /// Called when the app is started.
        /// </summary>
        public static void OnStart()
        {
            Setup.SetupDatabase();
        }
    }
}
