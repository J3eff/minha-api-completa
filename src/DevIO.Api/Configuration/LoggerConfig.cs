using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using DevIO.Api.Extensions;
using Microsoft.Extensions.Configuration;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLogginConfiguration(this IServiceCollection services, IConfiguration configuration)
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

            services.AddHealthChecks()
                .AddElmahIoPublisher("dceffe9764c84b9eb3fbba0f398104fc", new Guid("4ed7f66f-baab-45d3-bdc4-6b6e45ba5956"), "API Fornecedores")
                .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI();


            return services;
        }

        public static IApplicationBuilder  UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
