using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace MentorsBlog.Controllers
{
    public class BlogControllerBase : ControllerBase
    {
        [NonAction]
        public Guid GetUserId()
        {
            return Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
        }

        [NonAction]
        public JsonResult Json(object data)
        {
            return new JsonResult(data);
        }
    }
}