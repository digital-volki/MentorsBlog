using Microsoft.AspNetCore.Mvc;

namespace MentorsBlog.Controllers
{
    public class PingController : BlogControllerBase
    {
        /// <summary>
        /// Ping
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /happy
        ///
        /// </remarks>
        /// <returns>Ping</returns>
        /// <response code="200">Do you have a dream?</response>
        [Route("dream")]
        [HttpGet]
        public ActionResult Ping()
        {
            return Ok("Do you have a dream?");
        }
    }
}