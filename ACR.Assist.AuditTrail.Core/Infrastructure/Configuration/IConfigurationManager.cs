namespace Acr.Assist.AuditTrail.Core.Infrastructure.Configuration
{

    /// <summary>
    /// Interface for managing configuration related information
    /// </summary>
    public interface IConfigurationManager
    {

        /// <summary>
        /// Gets or sets the connection string
        /// </summary>
        string ConnectionString { get; }


        /// <summary>
        /// Get Application Title
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Get Applciation Version
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Get Applciation URL
        /// </summary>
        string ApplicationURL { get; }

        /// <summary>
        /// Get Route to Access Swagger
        /// </summary>
        string SwaggerRoutePrefix { get; }

        /// <summary>
        /// Gets the application files path.
        /// </summary>
        string ApplicationFilesPath { get; }

        /// <summary>
        /// Get Root Directory path
        /// </summary>
        string RootPath { get; }

        /// <summary>
        /// Gets or sets the authorization micro service URL
        /// </summary>
        string AuthorizationMicroServiceUrl { get; }

    }
}
