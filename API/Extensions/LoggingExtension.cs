using API.Properties.Configs;
using Serilog;

namespace API.Extensions
{
    public static class SerilogExtensions
    {
        public static IHostBuilder UseSerilogWithConfiguration(this IHostBuilder hostBuilder)
        {
            var configuration = LoggingConfigurationLoader.LoadConfiguration();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            SerilogConfig.ConfigureLogging(configuration, environment);

            return hostBuilder.UseSerilog();
        }
    }
}