using System.Collections.Generic;
using MentorsBlog.Application.Service.Models;
using MentorsBlog.Core.Common.Extensions;
using MentorsBlog.Core.DataAccess.Models;

namespace MentorsBlog.Application.Service.Mappers
{
    internal static class CommentMappers
    {
        #region Descending
        
        internal static DbComment ToDbComment(this Comment source)
        {
            return source == null ? default : new DbComment
            {
                Id = source.Id,
                PostId = source.PostId,
                ParentId = source.ParentId,
                Nickname = source.Nickname,
                Message = source.Message
            };
        }

        #endregion

        #region Ascending

        internal static Comment ToComment(this DbComment source)
        {
            return source == null ? default : new Comment
            {
                Id = source.Id,
                PostId = source.PostId,
                ParentId = source.ParentId,
                Nickname = source.Nickname,
                Message = source.Message,
                CreatedAt = source.CreatedAt,
            };
        }

        internal static IEnumerable<Comment> ToComment(this IEnumerable<DbComment> source)
        {
            return source?.MapToList(x => x.ToComment());
        }
        
        #endregion
    }
}