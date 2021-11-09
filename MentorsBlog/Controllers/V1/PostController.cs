using System;
using MentorsBlog.Models.Requests;
using MentorsBlog.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MentorsBlog.Application.Service.Interfaces;
using MentorsBlog.Mappers;
using Microsoft.AspNetCore.Http;

#pragma warning disable 1570

namespace MentorsBlog.Controllers.V1
{
    [Route("api/v1/post")]
    [ApiController]
    [Produces("application/json")]
    public class PostController : BlogControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Creating a post
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /post
        ///     {
        ///        "Title": "How create post?",
        ///        "Preview": "This is the body",
        ///        "Body": "This is the body of the post"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Incoming data for creating a post</param>
        /// <returns>The id of the created post</returns>
        /// <response code="201">Returns the id of the created post</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="403">No access</response>
        /// <response code="500">Failed to create post</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Guid> Create([FromBody, Required] RequestCreatePost request)
        {
            var post = _postService.Create(request.ToPost());
            return post != null
                ? Ok(post)
                : Problem();
        }

        /// <summary>
        /// Search for posts
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /post/search?searchData=Text
        ///
        /// </remarks>
        /// <param name="searchData">Search data</param>
        /// <returns>Search results</returns>
        /// <response code="200">Returns a list of found posts</response>
        /// <response code="400">Invalid input data</response>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<PostResponse>> Search([FromQuery, NotNull] string searchData)
        {
            return Ok(_postService
                .Search(searchData)
                .ToResponse());
        }

        /// <summary>
        /// Get post by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /post/f63225fb-cb9c-45d6-a750-901a47ab980c
        ///
        /// </remarks>
        /// <param name="id">Post Id</param>
        /// <returns>Post</returns>
        /// <response code="200">Returns a post find by id</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="404">Not fount post by id</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PostResponse> Get([FromRoute, Required] Guid id)
        {
            var post = _postService.Get(id);
            return post != null
                ? Ok(post.ToResponse())
                : NotFound();
        }

        /// <summary>
        /// Get messages by pagination, sorted by descending order of publication date
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /post?page=1&amp;count5
        ///
        /// </remarks>
        /// <param name="page">Page</param>
        /// <param name="count">Count of posts</param>
        /// <returns>Pagination result</returns>
        /// <response code="200">Returns a list of posts</response>
        /// <response code="400">Invalid input data</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<PostResponse>> GetByPagination([FromQuery, Required] int page, [FromQuery, Required] int count)
        {
            return Ok(_postService
                .Get(page, count)
                .OrderByDescending(x => x.PublishDate)
                .ToResponse());
        }

        /// <summary>
        /// Updating a post by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /post/f63225fb-cb9c-45d6-a750-901a47ab980c
        ///
        /// </remarks>
        /// <param name="id">Post id to update</param>
        /// <param name="request">Incoming data for update</param>
        /// <returns>Updated post</returns>
        /// <response code="200">Returns updated post</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="403">No access</response>
        /// <response code="500">Failed to update post</response>
        [HttpPut("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PostResponse> Update([FromRoute, Required] Guid id, [FromBody, Required] RequestUpdatePost request)
        {
            var post = _postService.Update(request.ToPost(id));
            return post != null 
                ? Ok(post.ToResponse()) 
                : Problem();
        }

        /// <summary>
        /// Deleting a post by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /post/f63225fb-cb9c-45d6-a750-901a47ab980c
        ///
        /// </remarks>
        /// <param name="id">Post id to delete</param>
        /// <returns>Success executable</returns>
        /// <response code="200">Returns the result success of execute</response>
        /// <response code="400">Invalid post id</response>
        /// <response code="403">No access</response>
        /// <response code="500">Failed to delete post</response>
        [HttpDelete("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete([FromRoute, Required] Guid id)
        {
            return _postService.Delete(id) 
                ? Ok()
                : Problem();
        }
    }
}