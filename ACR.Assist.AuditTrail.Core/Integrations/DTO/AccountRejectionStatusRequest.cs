namespace Acr.Assist.AuditTrail.Core.Integrations.DTO
{

    /// <summary>
    /// Contains the details for making a request to get the account status rejection
    /// status
    /// </summary>
    public class AccountRejectionStatusRequest
    {
        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        public string UserId { get; set; }



        /// <summary>
        /// Gets or sets the access token
        /// </summary>
        public string AccessToken { get; set; }
    }
}
