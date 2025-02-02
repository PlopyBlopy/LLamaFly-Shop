using Serilog;
using Serilog.Exceptions;

namespace API.Properties.Configs
{
    public class SerilogConfig
    {
        public static void ConfigureLogging(IConfiguration configuration, string environment)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithCorrelationId()
                .WriteTo.Elasticsearch(ElasticsearchConfig.ConfigureElasticSink(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

           
        }
    }
}

// Log.Logger = new LoggerConfiguration()
//.Enrich.FromLogContext() // Добавляет контекст в логи
//.WriteTo.Elasticsearch(ElasticsearchConfig.ConfigureElasticSink(configuration, environment)) // Настройка Elasticsearch
//.Enrich.WithProperty("Environment", environment) // Добавляет свойство "Environment" в логи
//.CreateLogger();