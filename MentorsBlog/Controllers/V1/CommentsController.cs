using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MentorsBlog.Application.Service.Interfaces;
using MentorsBlog.Mappers;
using MentorsBlog.Models.Requests;
using MentorsBlog.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace MentorsBlog.Controllers.V1
{
    [Route("api/v1/comments")]
    [ApiController]
    [Produces("application/json")]
    public class CommentsController : BlogControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Creating a comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /comments
        ///     {
        ///        "PostId": "13497b8d-3588-4826-9f87-d5ef0bdb5644"
        ///        "ParentId": null,
        ///        "Preview": "Anon without tyan 228",
        ///        "Body": "I'm first!"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Incoming data for creating a comment</param>
        /// <returns>Created comment</returns>
        /// <response code="200">Returns the model of the created comment</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="500">Failed to create comment</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CommentResponse> CreateComment([FromBody, Required] RequestCreateComment request)
        {
            var comment = _commentService.Create(request.ToComment());
            return comment != null
                ? Ok(comment.ToResponse())
                : Problem();
        }
        
        /// <summary>
        /// Get comments by post
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /comments?postId=c191c67b-7450-414d-8c8d-98efbd90dc59
        ///
        /// </remarks>
        /// <param name="postId">Post id</param>
        /// <returns>All comments</returns>
        /// <response code="200">Returns a list of comments</response>
        /// <response code="400">Invalid input data</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<CommentResponse>> Get([FromQuery, Required] Guid postId)
        {
            return Ok(_commentService
                .Get(postId)
                .OrderByDescending(x => x.CreatedAt)
                .ToResponse());
        }

        /// <summary>
        /// Deleting a comment by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /comments/f63225fb-cb9c-45d6-a750-901a47ab980c
        ///
        /// </remarks>
        /// <param name="id">Comment id to delete</param>
        /// <returns>Success executable</returns>
        /// <response code="200">Returns the result success of execute</response>
        /// <response code="400">Invalid comment id</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Failed to delete comment</response>
        [HttpDelete("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> Delete([FromRoute, Required] Guid id)
        {
            return _commentService.Delete(id) 
                ? Ok()
                : Problem();
        }
    }
}
