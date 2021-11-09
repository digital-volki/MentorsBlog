using System;
using System.Collections.Generic;
using MentorsBlog.Application.Service.Models;

namespace MentorsBlog.Application.Service.Interfaces
{
    public interface IPostService
    {
        Post Create(Post post);
        Post Get(Guid id);
        IEnumerable<Post> Get(int page, int count);
        IEnumerable<Post> Search(string searchData);
        Post Update(Post post);
        bool Delete(Guid id);
    }
}