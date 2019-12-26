using Acr.Assist.AuditTrail.Core.Infrastructure.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acr.Assist.AuditTrail.Controllers
{
    /// <summary>
    /// Base  class for all controllers
    /// </summary>
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Instance of the Logger
        /// </summary>
        protected ILogger<BaseController> LoggerInstance { get; set; }

        /// <summary>
        /// configuration
        /// </summary>
        public IConfigurationManager configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public BaseController(IConfigurationManager configuration)
        {
            this.configuration = configuration;
        }
    }
}
