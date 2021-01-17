namespace HealthPlace.Logic
{
    public static class Startup
    {
        /// <summary>
        /// Called when the app is started.
        /// </summary>
        public static void OnStartup()
        {
            HealthPlace.Core.Startup.OnStart();
        }

    }
}
