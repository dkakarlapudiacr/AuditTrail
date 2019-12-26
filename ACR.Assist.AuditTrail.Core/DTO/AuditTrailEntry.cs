using System;
using System.Collections.Generic;

namespace Acr.Assist.AuditTrail.Core.DTO
{
    public class AuditTrailEntry
    {
       public Guid LogID { get; set; }

        public string Username { get; set; }

        public string ModuleName { get; set; }

        public string ActionType { get; set; }

        public string Description { get; set; }

        public string Detailed_Description { get; set; }

        public DateTime LoggedTime { get; set; }



    }
}
