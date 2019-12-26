using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.Assist.AuditTrail.Core.DTO;
using Acr.Assist.AuditTrail.Core.Integrations.DTO;
using ACR.Assist.AuditTrail.Core.DTO;

namespace Acr.Assist.AuditTrail.Core.Services
{
    public interface IAuditTrailService
    {
        /// <summary>
        /// Adds the audit trail.
        /// </summary>
        /// <param name="audittrailentry">The audit trail entry.</param>
        /// <returns></returns>
        Task<AuditTrailEntry> AddAuditTrail(AddAuditTrailEntry audittrailentry);

    }
}
