using System.ComponentModel.DataAnnotations;
using MentorsBlog.Application.Service.Interfaces;
using MentorsBlog.Mappers;
using Microsoft.AspNetCore.Mvc;
using MentorsBlog.Models.Requests;
using Microsoft.AspNetCore.Http;

namespace MentorsBlog.Controllers.V1
{
    [Route("api/v1/user")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : BlogControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            var token = _userService.Authorization(request.ToUser());

            return !string.IsNullOrEmpty(token)
                ? Ok(token)
                : Unauthorized();
        }
        
        /// <summary>
        /// Account registration
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /user/reg
        ///     {
        ///        "Nickname": "admin",
        ///        "Password": "admin"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Incoming data for registration</param>
        /// <returns>Authentication token</returns>
        /// <response code="200">Returns the authentication token</response>
        /// <response code="400">Invalid input data</response>     
        /// <response code="409">An account with this nickname already exists</response>
        /// <response code="500">Failed to create account</response>
        [Route("reg")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> Registration([FromBody, Required] RequestRegistration request)
        {
            if (_userService.IsUserExist(request.Nickname))
            {
                return Conflict();
            }
            
            var token = _userService.Registration(request.ToUser());

            return !string.IsNullOrEmpty(token)
                ? Ok(token)
                : Problem();
        }
    }
}
