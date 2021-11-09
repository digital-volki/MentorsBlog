using System;
using System.Collections.Generic;
using System.Linq;
using MentorsBlog.Application.Domain.Interfaces;
using MentorsBlog.Application.Service.Interfaces;
using MentorsBlog.Application.Service.Mappers;
using MentorsBlog.Application.Service.Models;

namespace MentorsBlog.Application.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentDomain _commentDomain;

        public CommentService(ICommentDomain commentDomain)
        {
            _commentDomain = commentDomain;
        }
        
        public Comment Create(Comment comment)
        {
            if (comment == null)
            {
                return null;
            }
            
            var dbComment = _commentDomain.Create(comment.ToDbComment());
            return dbComment.ToComment();
        }

        public IEnumerable<Comment> Get(Guid postId)
        {
            return _commentDomain.Get(false)
                .Where(x => x.PostId == postId)
                .AsEnumerable()
                .ToComment();
        }

        public bool Delete(Guid id)
        {
            return _commentDomain.Delete(id);
        }
    }
}