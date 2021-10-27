using System.ComponentModel.DataAnnotations;

namespace MentorsBlog.Models.Requests
{
    public record RequestAuthorize
    {
        /// <summary>
        /// Account nickname
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Nickname { get; init; }

        /// <summary>
        /// Account password
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Password { get; init; }
    }
}