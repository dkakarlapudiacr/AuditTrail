
using System;
using System.Collections.Generic;
using System.Text;

namespace ACR.Assist.AuditTrail.Integrations
{
    /// <summary>
    /// Contains the account rejection status
    /// </summary>
    public class AccountRejectionStatus
    {
        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        public string UserId { get; set; }


        /// <summary>
        /// Gets or sets if the account is in rejected state
        /// </summary>
        public bool IsAccountInRejectedState { get; set; }
    }
}

