using System.Collections.Generic;

namespace Acr.Assist.AuditTrail.Core.Services.Validators
{

    /// <summary>
    /// Contains the details of the validation
    /// </summary>
    public class ValidatorResult
    {
        public ValidatorResult()
        {
            Errors = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets if the instance is valid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets the errors
        /// </summary>
        public Dictionary<string, string> Errors { get; }
    }
}
