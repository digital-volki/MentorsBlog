using MentorsBlog.Application.Service;
using MentorsBlog.Mappers;
using MentorsBlog.Models.Requests;
using MentorsBlog.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MentorsBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public PostController()
        {
            PostService.Init();
        }

        [HttpGet]
        public IEnumerable<PostResponse> Get([FromQuery] int page, [FromQuery] int count)
        {
            return PostService.Get(page, count).ToResponse();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Post([FromBody] RequestCreatePost request)
        {
            return Ok(request.Title);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
