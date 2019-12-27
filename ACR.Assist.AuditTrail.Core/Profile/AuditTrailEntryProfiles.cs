using Acr.Assist.AuditTrail.Core.DTO;
using ACR.Assist.AuditTrail.Core.DTO;

namespace Acr.Assist.AuditTrail.Core.Profile
{
    public class AuditTrailEntryProfiles : AutoMapper.Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditTrailEntryProfiles"/> class.
        /// </summary>
        public AuditTrailEntryProfiles()
        {
            CreateMap<AddAuditTrailEntry, AuditTrailEntry>();
        }
    }
}