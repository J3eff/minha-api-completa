using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLogginConfiguration(this IServiceCollection services)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "dceffe9764c84b9eb3fbba0f398104fc";
                o.LogId = new Guid("4ed7f66f-baab-45d3-bdc4-6b6e45ba5956");
            });

            //services.AddLogging(builder =>
            //{
            //    services.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "dceffe9764c84b9eb3fbba0f398104fc";
            //        o.LogId = new Guid("4ed7f66f-baab-45d3-bdc4-6b6e45ba5956");
            //    });

            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            return services;
        }

        public static IApplicationBuilder  UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
