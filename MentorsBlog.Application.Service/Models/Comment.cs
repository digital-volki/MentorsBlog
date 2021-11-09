using System;

namespace MentorsBlog.Application.Service.Models
{
    public class Comment
    {
        public Guid Id { get; init; }
        public Guid PostId { get; init; }
        public Guid? ParentId { get; init; }
        public string Nickname { get; init; }
        public string Message { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}