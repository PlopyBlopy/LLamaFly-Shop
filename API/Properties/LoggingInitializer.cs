using API.Properties.Configs;

namespace API.Properties
{
    public class LoggingInitializer
    {
        public static void Initialize()
        {
            var configuration = LoggingConfigurationLoader.LoadConfiguration();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            SerilogConfig.ConfigureLogging(configuration, environment);
        }
    }
}