using System;
using MentorsBlog.Application.Service.Models;
using MentorsBlog.Core.Common.Extensions;
using MentorsBlog.Models.Responses;
using System.Collections.Generic;
using MentorsBlog.Models.Requests;

namespace MentorsBlog.Mappers
{
    internal static class CommentMappers
    {
        #region Descending
        
        internal static Comment ToComment(this RequestCreateComment source)
        {
            return source == null ? default : new Comment
            {
                PostId = source.PostId,
                ParentId = source.ParentId,
                Nickname = source.Nickname,
                Message = source.Message
            };
        }
        
        #endregion

        #region Ascending

        internal static CommentResponse ToResponse(this Comment source)
        {
            return source == null ? default : new CommentResponse
            {
                Id = source.Id,
                PostId = source.PostId,
                ParentId = source.ParentId,
                Nickname = source.Nickname,
                Message = source.Message,
                CreateDate = source.CreatedAt,
            };
        }

        internal static IEnumerable<CommentResponse> ToResponse(this IEnumerable<Comment> source)
        {
            return source?.MapToList(x => x.ToResponse());
        }
        
        #endregion
    }
}