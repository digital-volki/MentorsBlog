using System;

namespace MentorsBlog.Application.Service.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        public string Body { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
