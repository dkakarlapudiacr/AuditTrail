using System;
using System.Collections.Generic;
using System.Text;

namespace Acr.Assist.AuditTrail.Core.Exception
{
    /// <summary>
    /// Exception thrown  input validation failures
    /// </summary>
    public class InputValidationFailureException : ApplicationException
    {
        /// <summary>
        /// Gets or sets the list of errors
        /// </summary>
        public Dictionary<string, string> Errors { get; }

        public InputValidationFailureException(string message) : base(message)
        {
        }

        public InputValidationFailureException(Dictionary<string, string> errors) : base()
        {
            Errors = errors;
        }

        public InputValidationFailureException(string message, Dictionary<string, string> errors) : base(message)
        {
            Errors = errors;
        }

        public InputValidationFailureException(string message, System.Exception innerException) : base(message, innerException)
        {
        }




    }
}
