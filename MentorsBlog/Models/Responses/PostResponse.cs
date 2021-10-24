using System;

namespace MentorsBlog.Models.Responses
{
    public record PostResponse
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Preview { get; init; }
        public string Body { get; init; }
        public DateTime PublishDate { get; init; }
    }
}
