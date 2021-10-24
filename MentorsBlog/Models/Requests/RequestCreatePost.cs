using System.ComponentModel.DataAnnotations;

namespace MentorsBlog.Models.Requests
{
    public record RequestCreatePost
    {
        [Required]
        [MaxLength(5)]
        public string Title { get; init; }

        [Required]
        [MaxLength(2000)]
        public string Preview { get; init; }

        [Required]
        [MaxLength(20000)]
        public string Body { get; init; }
    }
}
