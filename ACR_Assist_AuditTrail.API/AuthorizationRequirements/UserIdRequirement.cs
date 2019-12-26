using Microsoft.AspNetCore.Authorization;

namespace Acr.Assist.AuditTrail
{

    /// <summary>
    /// Represensts the requirement for user Id 
    /// </summary>
    public class UserIdRequirement : IAuthorizationRequirement
    {

        public string UserIdClaim { get; }

        public UserIdRequirement(string userIdClaim)
        {
            UserIdClaim = userIdClaim;
        }

    }
}
