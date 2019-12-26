using System;
using System.Threading.Tasks;
using Acr.Assist.AuditTrail.Core.Integrations;
using Acr.Assist.AuditTrail.Core.Infrastructure.Configuration;
using Acr.Assist.AuditTrail.Core.Services;
using ACR.Assist.AuditTrail.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Acr.Assist.AuditTrail.Controllers
{
    [Produces("application/json")]
    [Route("audittrail/api/v1")]
    [Consumes("application/json")]
    [Authorize(Policy = "UserIdExists")]
    public class AuditTrailController : Controller
    {
        /// <summary>
        /// The audit trail service
        /// </summary>
        private IAuditTrailService audittrailService;
        /// <summary>
        /// The authorization micro service
        /// </summary>
        private readonly IAuthorizationMicroService authorizationMicroService;


        /// <summary>
        /// Initializes a new instance of the <see cref="audittrailController"/> class.
        /// </summary>
        /// <param name="audittrailService">The audit trail service.</param>
        /// <param name="authorizationMicroService">The authorization micro service.</param>
        /// <param name="configuration">The configuration.</param>
        public AuditTrailController(
            IAuditTrailService audittrailService,
            IAuthorizationMicroService authorizationMicroService,
             IConfigurationManager configuration
            )
        {
            this.audittrailService = audittrailService;
            this.authorizationMicroService = authorizationMicroService;
        }



        /// <summary>
        /// Adds audittrail for every action
        /// </summary>
        /// <param name="audittrailEntry">Contains the addAuditTrailEntry</param>
        /// <returns></returns>
        [Route("AuditTrail")]
        [HttpPost]
        public async Task<IActionResult> AddAuditTrail([FromBody]AddAuditTrailEntry audittrailEntry)
        {
            var res = await audittrailService.AddAuditTrail(audittrailEntry);
            return Ok(res);
        }

    }
}