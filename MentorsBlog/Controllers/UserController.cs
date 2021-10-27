using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MentorsBlog.Models.Requests;
using Microsoft.AspNetCore.Http;

namespace MentorsBlog.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : BlogControllerBase
    {
        public UserController()
        {
            
        }
        
        /// <summary>
        /// Authorize in account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /user/auth
        ///     {
        ///        "Nickname": "admin",
        ///        "Password": "admin"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Incoming data for authorize</param>
        /// <returns>Authentication token</returns>
        /// <response code="200">Returns the authentication token</response>
        /// <response code="400">Invalid input data</response>     
        /// <response code="401">Unauthorized</response>
        [Route("auth")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<string> Authorize([FromBody, Required] RequestAuthorize request)
        {
            var token = string.Empty;

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            
            return Ok(token);
        }
    }
}
