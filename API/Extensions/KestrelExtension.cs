namespace API.Extensions
{
    public static class KestrelExtension
    {
        public static IWebHostBuilder AddKestrelWebHost(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureKestrel(options =>
            {
                options.ConfigureHttpsDefaults(httpsOptions =>
                {
                    httpsOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                });
            });

            return webHostBuilder;
        }
    }
}