using System.Collections.Generic;
using MentorsBlog.Application.Service.Models;
using MentorsBlog.Core.Common.Extensions;
using MentorsBlog.Core.DataAccess.Models;

namespace MentorsBlog.Application.Service.Mappers
{
    internal static class PostMappers
    {
        #region Descending
        
        internal static DbPost ToDbPost(this Post source)
        {
            return source == null ? default : new DbPost
            {
                Id = source.Id,
                Title = source.Title,
                Preview = source.Preview,
                Body = source.Body,
                Image = source.Image,
                PublishDate = source.PublishDate,
            };
        }

        #endregion

        #region Ascending

        internal static Post ToPost(this DbPost source)
        {
            return source == null ? default : new Post
            {
                Id = source.Id,
                Title = source.Title,
                Preview = source.Preview,
                Body = source.Body,
                Image = source.Image,
                PublishDate = source.PublishDate,
            };
        }

        internal static IEnumerable<Post> ToPost(this IEnumerable<DbPost> source)
        {
            return source?.MapToList(x => x.ToPost());
        }
        
        #endregion
    }
}