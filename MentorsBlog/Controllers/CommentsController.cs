using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MentorsBlog.Models.Requests;
using MentorsBlog.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace MentorsBlog.Controllers
{
    [Route("api/v1/comments")]
    [ApiController]
    [Produces("application/json")]
    public class CommentsController : BlogControllerBase
    {
        public CommentsController()
        {
            
        }

        /// <summary>
        /// Creating a comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /comments
        ///     {
        ///        "ParentId": null,
        ///        "Preview": "Anon without tyan 228",
        ///        "Body": "I'm first!"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Incoming data for creating a comment</param>
        /// <returns>Created comment</returns>
        /// <response code="200">Returns the id of the created post</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="500">Failed to create comment</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CommentResponse> CreateComment(RequestCreateComment request)
        {
            var comment = new object(); 
            
            if (comment == null)
            {
                return Problem();
            }
            
            return Ok(new CommentResponse());
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
        public ActionResult<IEnumerable<CommentResponse>> Get([FromQuery] Guid postId)
        {
            return Ok(new List<CommentResponse>());
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
        /// <response code="403">No access</response>
        /// <response code="500">Failed to delete comment</response>
        [HttpDelete("{id:guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> Delete(Guid id)
        {
            var result = true;

            if (!result)
            {
                return Problem();
            }
            
            return Ok();
        }
    }
}
