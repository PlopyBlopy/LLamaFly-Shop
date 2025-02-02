using API.Properties;
using API.Properties.Configs;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace API.Services.Extensions
{
    public static class SerilogExtensions
    {
        public static IHostBuilder UseSerilogWithConfiguration(this IHostBuilder hostBuilder)
        {
            // Инициализация логгера
            LoggingInitializer.Initialize();

            // Использование Serilog в Host
            return hostBuilder.UseSerilog();
        }
    }
}