using System;
using System.Collections.Generic;
using System.Text;

namespace ACR.Assist.AuditTrail.Core.DTO
{
   public class AddAuditTrailEntry
    {
        public string Username { get; set; }

        public string ModuleName { get; set; }

        public string ActionType { get; set; }

        public string Description { get; set; }

        public string Detailed_Description { get; set; }

    }
}
