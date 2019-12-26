using System;
using Acr.Assist.AuditTrail.Core.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace Acr.Assist.AuditTrail.Infrastructure
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfiguration configuration;

        public ConfigurationManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string ConnectionString => configuration.GetConnectionString("MarvalDatabase");

        public string Title => configuration["Title"];

        public string Version => configuration["Version"];

        public string ApplicationURL => configuration["Environment:ApplicationURL"];

        public string SwaggerRoutePrefix => configuration["Environment:SwaggerRoutePrefix"];

        public string ApplicationFilesPath => configuration["Environment:ApplicationFilesPath"];

        public string RootPath => AppDomain.CurrentDomain.BaseDirectory;

        public string AuthorizationMicroServiceUrl => configuration["Integrations:AuthorizationService"];

    }
}