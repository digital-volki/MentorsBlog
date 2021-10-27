using System;
using System.ComponentModel.DataAnnotations;

namespace MentorsBlog.Models.Requests
{
    public record RequestCreateComment
    {
        /// <summary>
        /// Id of the parent comment
        /// </summary>
        public Guid? ParentId { get; init; }

        /// <summary>
        /// Anonymous nickname
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Nickname { get; init; }

        /// <summary>
        /// Comment message
        /// </summary>
        [Required]
        [MaxLength(1000-7)]
        public string Message { get; init; }
    }
}
