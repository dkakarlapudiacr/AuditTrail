using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acr.Assist.AuditTrail.Controllers
{
    /// <summary>
    /// Testing operations handled by this controller
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Produces("application/json")]
    [Route("api/HelloWorld")]
    [AllowAnonymous]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        /// <summary>
        /// Method which determines if the service is running
        /// </summary>
        /// <returns>Returns Hello World if service is running</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult HelloWorld()
        {
            return Ok("Hello  World");
        }
    }
}