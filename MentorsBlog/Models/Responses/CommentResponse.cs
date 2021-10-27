using System;

namespace MentorsBlog.Models.Responses
{
    public record CommentResponse
    {
        /// <summary>
        /// Comment id
        /// </summary>
        public Guid Id { get; init; }
        
        /// <summary>
        /// A post containing this comment
        /// </summary>
        public Guid PostId { get; init; }
        
        /// <summary>
        /// Id of the parent comment
        /// </summary>
        public Guid? ParentId { get; init; }
        
        /// <summary>
        /// Anonymous nickname
        /// </summary>
        public string Nickname { get; init; }
        
        /// <summary>
        /// Comment message
        /// </summary>
        public string Message { get; init; }
        
        /// <summary>
        /// Date the comment was created
        /// </summary>
        public DateTime CreateDate { get; init; }
    }
}