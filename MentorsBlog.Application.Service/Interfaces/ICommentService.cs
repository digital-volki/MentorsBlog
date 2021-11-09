using System;
using System.Collections.Generic;
using MentorsBlog.Application.Service.Models;

namespace MentorsBlog.Application.Service.Interfaces
{
    public interface ICommentService
    {
        Comment Create(Comment comment);
        IEnumerable<Comment> Get(Guid postId);
        bool Delete(Guid id);
    }
}