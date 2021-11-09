using System;

namespace MentorsBlog.Application.Service.Models
{
    public class User
    {
        public Guid Id { get; init; }
        public string Nickname { get; init; }
        public string Password { get; init; }
    }
}