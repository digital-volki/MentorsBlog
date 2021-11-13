using System.ComponentModel.DataAnnotations;

namespace MentorsBlog.Models.Requests
{
    public record RequestUpdatePost
    {
        /// <summary>
        /// Title of the post
        /// </summary>
        [MaxLength(200)]
        public string Title { get; init; }

        /// <summary>
        /// Preview of the post 
        /// </summary>
        [MaxLength(2000)]
        public string Preview { get; init; }

        /// <summary>
        /// The content of the post
        /// </summary>
        [MaxLength(20000)]
        public string Body { get; init; }
        
        /// <summary>
        /// The link to image of the post
        /// </summary>
        [MaxLength(500)]
        public string Image { get; init; }
    }
}
