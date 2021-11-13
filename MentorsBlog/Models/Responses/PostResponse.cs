using System;

namespace MentorsBlog.Models.Responses
{
    public record PostResponse
    {
        /// <summary>
        /// Post id
        /// </summary>
        public Guid Id { get; init; }
        
        /// <summary>
        /// Title of the post
        /// </summary>
        public string Title { get; init; }
        
        /// <summary>
        /// Preview of the post 
        /// </summary>
        public string Preview { get; init; }
        
        /// <summary>
        /// The content of the post
        /// </summary>
        public string Body { get; init; }
        
        /// <summary>
        /// The link to image of the post
        /// </summary>
        public string Image { get; init; }
        
        /// <summary>
        /// Date the post was created
        /// </summary>
        public DateTime PublishDate { get; init; }
    }
}
