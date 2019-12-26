using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.Assist.AuditTrail.Core.Integrations.DTO;

namespace Acr.Assist.AuditTrail.Core.Integrations
{
    public interface IAuthorizationMicroService
    {

        /// <summary>
        /// Checks if the user with  user account is rejecetd
        /// </summary>
        /// <param name="accountRejectionStatusRequest"> Contains the details for making a request to get the account status rejection</param>
        /// <returns>True if the User is rejected else it returns alse </returns>
        Task<bool> CheckIfUserIsRejected(AccountRejectionStatusRequest accountRejectionStatusRequest);
    }
}
