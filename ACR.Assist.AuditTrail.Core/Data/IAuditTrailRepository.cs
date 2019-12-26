using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.Assist.AuditTrail.Core.DTO;

namespace ACR.Assist.AuditTrail.Core.Data
{
    public interface IAuditTrailRepository
    {
        /// <summary>
        /// Adds Audit Trail
        /// </summary>
        /// <param name="audittrail">AuditTrail.</param>
        /// <returns></returns>
        Task<Guid> AddAuditTrail(AuditTrailEntry audittrail);

     
    }
}
